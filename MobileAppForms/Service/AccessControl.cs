using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobileAppForms.Extension;
using MobileAppForms.Model;
using MobileAppForms.Store;
using MobileAppForms.Vue;
using VueSharp;
using Xamarin.Forms;

namespace MobileAppForms.Service
{
    public class AccessControl
    {
        #region properties

        private InternalStore Store { get; }
        private Router Router { get; }
        private SigninFlow SigninFlow { get; }

        private bool IsFirstAccess { get; set; }

        private bool IsActing { get; set; }
        public bool IsModalPresented { get; private set; }
        public bool IsSigninActing { get; private set; }

        #endregion

        #region ctor(s)

        public AccessControl(InternalStore store, Router router, SigninFlow signinFlow)
        {
            Store = store;
            Router = router;
            SigninFlow = signinFlow;

            // IsFirstAccess = true;
        }

        #endregion

        #region AccessControl implementation

        public bool IsUserSignedIn => !IsFirstAccess && Store.State.ProfileState.CurrentUtilizer is Customer;

        public bool CanShowComponent(Component component)
        {
            var canShowComponent = false;

            if (!(component is AnonymousDeniedComponent))
            {
                canShowComponent = true;
            }
            else
            {
                if (!IsFirstAccess)
                {
                    var credentials = Store.State.ProfileState.CurrentCustomerCredentials;
                    canShowComponent = credentials.HasValue;
                }
            }

            return canShowComponent;
        }

        public async Task<bool> ComponentRequested(Component component)
        {
            IsActing = true;

            var canShowComponent = CanShowComponent(component);

            if (!canShowComponent)
            {
                bool shouldSigninOrSignup;

                var credentials = Store.State.ProfileState.CurrentCustomerCredentials;
                if (credentials.HasValue)
                {
                    shouldSigninOrSignup = true;
                }
                else
                {
                    IsModalPresented = true;

                    var state = new AuthenticationRequiredPopupComponent.ComponentState(component, false);
                    shouldSigninOrSignup = await Router.PresentComponent<AuthenticationRequiredPopupComponent, AuthenticationRequiredPopupComponent.ComponentState, bool>(state);

                    IsModalPresented = false;
                }

                if (shouldSigninOrSignup)
                {
                    canShowComponent = await PerformSignin();
                }
            }

            if (canShowComponent)
            {
                IsFirstAccess = false;
            }

            IsActing = false;
            return canShowComponent;
        }

        public async Task<bool> ForcePerformSignin()
        {
            IsActing = true;

            bool signinCompleted = await PerformSignin();
            if (signinCompleted)
            {
                IsFirstAccess = false;
            }

            IsActing = false;

            return signinCompleted;
        }

        #endregion


        #region helper method

        private async Task<bool> PerformSignin()
        {
            IsSigninActing = true;

            await SigninFlow.HandleSigninFlow(true);

            _ = Store.Dispatch(nameof(UpdateTabsUi));

            bool canShowComponent = Store.ProfileModule.State.CurrentUtilizer is Customer;

            IsSigninActing = false;

            return canShowComponent;
        }

        #endregion
    }
}
