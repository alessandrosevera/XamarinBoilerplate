using System;
using Android.Views;
using MobileAppForms.Droid;
using MobileAppForms.Service;
using MobileAppForms.Ux;
using MobileAppForms.Vue;
using VueSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace MobileAppForms.Listener
{
    public class NavigationItemSelectedListener : Java.Lang.Object, Google.Android.Material.Navigation.NavigationBarView.IOnItemSelectedListener
    {
        #region auto-properties

        RootPage RootPage { get; set; }

        #endregion

        #region ctor(s)

        public NavigationItemSelectedListener()
        {
        }

        #endregion

        #region IOnItemSelectedListener implementations

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            if (RootPage is null)
                return false;

            var accessControl = MainActivity.Container.Resolve<AccessControl>();
            if (accessControl.IsModalPresented)
            {
                var componentFactory = MainActivity.Container.Resolve<ComponentFactory>();
                var authenticationPopupComponent = componentFactory.CreateComponent<AuthenticationRequiredPopupComponent>();
                if (authenticationPopupComponent != null)
                {
                    authenticationPopupComponent.Unpresent();
                }
            }

            bool canGo = false;

            try
            {
                var page = RootPage.Children[item.ItemId];
                if (!(page is null) && page != RootPage.CurrentPage)
                {
                    var rootComponent = MainActivity.Container.Resolve<SharedRootComponent>();
                    canGo = rootComponent.CanSelectTabPage(page);
                    _ = rootComponent.TryAndSelectTabPage(page);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                // TODO LOG
            }

            return canGo;
        }

        #endregion

        #region access methods

        public void Configure(RootPage rootPage)
        {
            RootPage = rootPage;
        }

        #endregion
    }
}

