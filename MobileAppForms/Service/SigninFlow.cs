using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobileAppForms.Model;
using MobileAppForms.Provider.Core;
using MobileAppForms.Store;
using MobileAppForms.Vue;
using VueSharp;
using Xamarin.Forms;

namespace MobileAppForms.Service
{
    public class SigninFlow
    {
        #region auto-properties

        private Router Router { get; }
        private InternalStore Store { get; }
        private UtilizerProvider UtilizerProvider { get; }

        #endregion

        #region ctor(s)

        public SigninFlow(Router router, InternalStore store, UtilizerProvider utilizerProvider)
        {
            Store = store;
            Router = router;
            UtilizerProvider = utilizerProvider;
        }

        #endregion

        #region access methods

        public async Task HandleSigninFlow(bool isAlreadyEnteredInAppContext = false)
        {
            bool didExit = false;
            while (true)
            {
                await Store.Dispatch(nameof(LoadCurrentUtilizer));
                await Store.Dispatch(nameof(LoadConfiguration));

                var anonymous = await UtilizerProvider.GetAnonymous();
                Store.Commit(nameof(SetCurrentUtilizer), anonymous);
                Store.Commit(nameof(SetCurrentCustomerCredentials), null);

                CreateTabItems(SigninResult.EnterAnonymous);

                didExit = true;

                if (didExit)
                {
                    break;
                }
            }

            if (!isAlreadyEnteredInAppContext)
            {
                _ = Router.PresentComponent<SharedRootComponent, bool, bool>(true);
            }
        }

        #endregion

        #region helper methods

        private void CreateTabItems(SigninResult signinResult)
        {
            Dictionary<AppTabs, TabItemState> tabItems = new Dictionary<AppTabs, TabItemState>();
            tabItems.Add(AppTabs.Search, new TabItemState()
            {
                Icon = "icon_tab_home",
                DisabledIcon = "icon_tab_home_disabled",
                SelectedIcon = "icon_tab_home",
                SelectedColor = (Color)Application.Current.Resources["MainColor"],
                DisabledColor = (Color)Application.Current.Resources["DustyGray40"],
                UnselectedColor = (Color)Application.Current.Resources["DustyGray"]
            });
            tabItems.Add(AppTabs.Favorites, new TabItemState()
            {
                Icon = "icon_tab_favorite",
                DisabledIcon = "icon_tab_favorite_disabled",
                SelectedIcon = "icon_tab_favorite",
                SelectedColor = (Color)Application.Current.Resources["MainColor"],
                DisabledColor = (Color)Application.Current.Resources["DustyGray40"],
                UnselectedColor = (Color)Application.Current.Resources["DustyGray"],
                IsDisabled = signinResult == SigninResult.EnterAnonymous
            });
            tabItems.Add(AppTabs.Agency, new TabItemState()
            {
                Icon = "icon_tab_agency",
                DisabledIcon = "icon_tab_agency_disabled",
                SelectedIcon = "icon_tab_agency",
                SelectedColor = (Color)Application.Current.Resources["MainColor"],
                DisabledColor = (Color)Application.Current.Resources["DustyGray40"],
                UnselectedColor = (Color)Application.Current.Resources["DustyGray"]
            });
            tabItems.Add(AppTabs.Profile, new TabItemState()
            {
                Icon = "icon_tab_profile",
                DisabledIcon = "icon_tab_profile_disabled",
                SelectedIcon = "icon_tab_profile",
                SelectedColor = (Color)Application.Current.Resources["MainColor"],
                DisabledColor = (Color)Application.Current.Resources["DustyGray40"],
                UnselectedColor = (Color)Application.Current.Resources["DustyGray"],
                IsDisabled = signinResult == SigninResult.EnterAnonymous
            });

            Store.Commit(nameof(SetTabItems), tabItems);
        }

        #endregion
    }
}
