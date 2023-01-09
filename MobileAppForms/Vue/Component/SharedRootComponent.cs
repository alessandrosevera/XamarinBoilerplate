using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobileAppForms.Abstraction;
using MobileAppForms.Extension;
using MobileAppForms.Model;
using MobileAppForms.Service;
using MobileAppForms.Ux;
using VueSharp;
using VueSharp.Abstraction;
using VueSharp.Model.Core;
using VueSharp.Provider.Core;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppForms.Vue
{
    public class SharedRootComponent : RootComponent
    {
        #region auto-properties

        private ComponentFactory ComponentFactory { get; }
        private SharedRouter Router { get; }
        private AccessControl AccessControl { get; }

        private List<Component> Components { get; }

        private bool IsSettingCurrentTabComponent { get; set; }

        public Page LastSelectedPage { get; private set; }

        #endregion

        #region properties

        private RootPage RootPresenter => Presenter as RootPage;

        #endregion

        #region ctor(s)

        public SharedRootComponent(ComponentFactory componentFactory, Router router, AccessControl accessControl)
        {
            ComponentFactory = componentFactory;
            AccessControl = accessControl;

            Router = router as SharedRouter;

            Components = new List<Component>();
        }

        #endregion

        #region abstract methods implementation

        protected override Task Configure(bool state)
        {
            RootPresenter.CurrentPageChanged += HandleCurrentPageChanged;

            return Task.CompletedTask;
        }

        public override void ApplyLocalization(NGettext.ICatalog catalog)
        {
            base.ApplyLocalization(catalog);

            if (!(Components is null) && Components.Count > 0)
            {
                foreach (var component in Components)
                {
                    var localizableComponent = component as LocalizableElement;
                    if (!(localizableComponent is null))
                    {
                        if (!(catalog is null))
                        {
                            localizableComponent.ApplyLocalization(catalog);
                        }
                    }
                }
            }
        }

        protected override Presenter CreatePresenter()
        {
            return new RootPage();
        }

        protected override async Task Initialize(bool state)
        {
            await BindComponents();
        }

        protected override Task PresentInternal()
        {
            // _ = CurrentPageChangedInternal();
            PresentCurrentComponent();

            return Task.CompletedTask;
        }

        #endregion

        #region access methods

        internal void UpdateTabsUi()
        {
            RootPresenter?.RaiseUpdateTabsUi();
        }

        #endregion

        #region helper methods

        private async Task BindComponents()
        {
            await BindComponent<SearchComponent, bool>(RootPresenter.SearchPage);
            await BindComponent<FavoritesComponent, bool>(RootPresenter.FavoritesPage);
            await BindComponent<AgencyComponent, bool>(RootPresenter.AgencyPage);
            await BindComponent<ProfileComponent, bool>(RootPresenter.ProfilePage);
        }

        private async Task BindComponent<C, S>(Presenter ownLayout) where C : SharedTabComponent<S>
        {
            var component = ComponentFactory.CreateComponent<C>();
            component.InsertPresenter(ownLayout);
            await component.Prepare(default(S));

            Components.Add(component);
        }

        private void PresentCurrentComponent()
        {
            if (!(Components is null) && Components.Count > 0)
            {
                var currentComponent = Components.FirstOrDefault(c => c.Presenter == RootPresenter.CurrentPage);
                if (!(currentComponent is null) && currentComponent != Router.CurrentTabComponent)
                {
                    PresentComponentInternal(currentComponent);
                }
            }
        }

        private void PresentComponentInternal<TComponent, TState>(TState state)
            where TComponent : SharedTabComponent<TState>
        {
            var component = Components.FirstOrDefault(c => c is TComponent);

            _ = Router.PresentComponent<TComponent, TState, bool>(state);

            LastSelectedPage = component.Presenter as Page;
        }

        private void PresentComponentInternal(Component component)
        {
            switch (component)
            {
                case AgencyComponent:
                    _ = Router.PresentComponent<AgencyComponent, bool, bool>(true);
                    break;
                case FavoritesComponent:
                    _ = Router.PresentComponent<FavoritesComponent, bool, bool>(true);
                    break;
                case ProfileComponent:
                    _ = Router.PresentComponent<ProfileComponent, bool, bool>(true);
                    break;
                case SearchComponent:
                    _ = Router.PresentComponent<SearchComponent, bool, bool>(true);
                    break;
            }

            LastSelectedPage = component.Presenter as Page;
        }

        #endregion

        #region event handlers

        private void HandleCurrentPageChanged(object sender, EventArgs e)
        {
            // CurrentPageChangedInternal();
            PresentCurrentComponent();
        }

        #endregion

        #region predicates

        public bool CanSelectTabComponent<C>() where C : Component
        {
            var component = ComponentFactory.CreateComponent<C>();
            if (!(component is null) && component.Presenter is Page componentPage)
            {
                var requestedComponent = Components.FirstOrDefault(c => c.MatchComponentPresenter(componentPage));
                if (!(requestedComponent is null))
                {
                    return AccessControl.CanShowComponent(requestedComponent);
                }
            }
            return false;
        }

        public async Task TryAndSelectTabComponent<C, TState>(TState state) where C : SharedTabComponent<TState>
        {
            var component = ComponentFactory.CreateComponent<C>();
            if (!(component is null) && component.Presenter is Page componentPage)
            {
                var requestedComponent = Components.FirstOrDefault(c => c.MatchComponentPresenter(componentPage));
                if (!(requestedComponent is null))
                {
                    var canGo = await AccessControl.ComponentRequested(requestedComponent);
                    if (canGo)
                    {
                        PresentComponentInternal<C, TState>(state);
                        LastSelectedPage = componentPage;
                        RootPresenter.CurrentPage = componentPage;
                    }
                    else
                    {
                        if (LastSelectedPage is null)
                        {
                            var firstAvailableComponent = Components.FirstOrDefault(c => !(c is AnonymousDeniedComponent));
                            PresentComponentInternal(firstAvailableComponent);
                        }
                    }
                }
                else
                {
                    if (LastSelectedPage is null)
                    {
                        var firstAvailableComponent = Components.FirstOrDefault(c => !(c is AnonymousDeniedComponent));
                        PresentComponentInternal(firstAvailableComponent);
                    }
                }
            }
        }

        public bool CanSelectTabPage(Page page)
        {
            var requestedComponent = Components.FirstOrDefault(c => c.MatchComponentPresenter(page));
            if (!(requestedComponent is null))
            {
                return AccessControl.CanShowComponent(requestedComponent);
            }
            return false;
        }

        public async Task TryAndSelectTabPage(Page page)
        {
            var requestedComponent = Components.FirstOrDefault(c => c.MatchComponentPresenter(page));
            if (!(requestedComponent is null))
            {
                var canGo = await AccessControl.ComponentRequested(requestedComponent);
                if (canGo)
                {
                    PresentComponentInternal(requestedComponent);
                    LastSelectedPage = page;
                    RootPresenter.CurrentPage = page;
                }
                else
                {
                    if (LastSelectedPage is null)
                    {
                        var firstAvailableComponent = Components.FirstOrDefault(c => !(c is AnonymousDeniedComponent));
                        PresentComponentInternal(firstAvailableComponent);
                    }
                }
            }
            else
            {
                if (LastSelectedPage is null)
                {
                    var firstAvailableComponent = Components.FirstOrDefault(c => !(c is AnonymousDeniedComponent));
                    PresentComponentInternal(firstAvailableComponent);
                }
            }
        }

        #endregion
    }
}