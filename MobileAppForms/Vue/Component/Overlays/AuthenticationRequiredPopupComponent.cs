using System;
using System.Threading.Tasks;
using MobileAppForms.Ux.Phone;
using VueSharp;
using VueSharp.Abstraction;
using VueSharp.Provider.Core;
using Xamarin.Forms;

namespace MobileAppForms.Vue
{
    public class AuthenticationRequiredPopupComponent : OverlayComponent<AuthenticationRequiredPopupComponent.ComponentState, bool>
    {
        #region nested classes

        public readonly struct ComponentState
        {
            public Component RequestedComponent { get; }
            public bool IsAction { get; }

            public ComponentState(Component requestedComponent, bool isAction)
            {
                RequestedComponent = requestedComponent;
                IsAction = isAction;
            }
        }

        #endregion

        #region auto-properties

        private CatalogProvider CatalogProvider { get; }

        #endregion

        #region properties

        public override View Backdrop => (Presenter as AuthenticationRequiredPopupPresenter)?.Backdrop;
        private AuthenticationRequiredPopupPresenter OwnPresenter => Presenter as AuthenticationRequiredPopupPresenter;

        #endregion

        #region ctor(s)

        public AuthenticationRequiredPopupComponent(CatalogProvider catalogProvider)
        {
            CatalogProvider = catalogProvider;
        }

        #endregion

        #region overrides

        protected override Task Configure(ComponentState state)
        {
            OwnPresenter?.Configure(HandleOutsideAreaTapped, HandleSignInClicked);
            return Task.CompletedTask;
        }

        protected override Presenter CreatePresenter()
        {
            return new AuthenticationRequiredPopupPresenter();
        }

        protected override async Task Initialize(ComponentState state)
        {
            var catalog = await CatalogProvider.GetLocalCatalog();
            string requestedTabName = string.Empty;
            switch (state.RequestedComponent)
            {
                case ProfileComponent:
                    requestedTabName = catalog.GetString("Profilo");
                    break;
                case SearchComponent:
                    requestedTabName = catalog.GetString("Cerca");
                    break;
                case AgencyComponent:
                    requestedTabName = catalog.GetString("Agenzia");
                    break;
                case FavoritesComponent:
                    requestedTabName = catalog.GetString("Preferiti");
                    break;
            }

            OwnPresenter?.Initialize(requestedTabName);
        }

        protected override Task PresentInternal()
        {
            OwnPresenter?.Present();
            return Task.CompletedTask;
        }

        #endregion

        #region event handlers

        private async void HandleOutsideAreaTapped()
        {
            await OwnPresenter?.Dismiss();
            CompletionSource?.TrySetResult(false);
        }

        private async void HandleSignInClicked()
        {
            await OwnPresenter?.Dismiss();
            CompletionSource?.TrySetResult(true);
        }

        #endregion
    }
}
