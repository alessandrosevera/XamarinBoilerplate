using System;
using Android.Views;
using MobileAppForms.Ux;

namespace MobileAppForms.Listener
{
    public class NavigationItemReselectedListener : Java.Lang.Object, Google.Android.Material.Navigation.NavigationBarView.IOnItemReselectedListener
    {
        #region auto-properties

        RootPage RootPage { get; set; }

        #endregion

        #region ctor(s)

        public NavigationItemReselectedListener()
        {
        }

        #endregion

        #region IOnItemReselectedListener implementations

        public void OnNavigationItemReselected(IMenuItem p0)
        {
            return;
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