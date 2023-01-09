using System;
using System.Threading.Tasks;
using MobileAppForms.Service;
using MobileAppForms.Store;
using MobileAppForms.Ux.Phone;
using VueSharp;
using VueSharp.Abstraction;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppForms.Vue
{
    public class UnknownCustomerCredentialsPopupComponent : OverlayComponent<bool, UnknownCustomerCredentialsPopupComponent.ComponentResult>
    {
        #region nested classes

        public readonly struct ComponentResult
        {
            public ActionType ActionType { get; }

            public ComponentResult(ActionType actionType)
            {
                ActionType = actionType;
            }
        }

        public enum ActionType
        {
            GoToHome,
            SignIn
        }

        #endregion

        #region auto-properties

        private Router Router { get; }
        private InternalStore Store { get; }
        private SigninFlow SigninFlow { get; }

        #endregion

        #region properties

        public override View Backdrop => (Presenter as UnknownCustomerCredentialsOverlayPresenter)?.Backdrop;
        private UnknownCustomerCredentialsOverlayPresenter OwnPresenter => Presenter as UnknownCustomerCredentialsOverlayPresenter;

        private SharedRouter SharedRouter => Router as SharedRouter;

        #endregion

        #region ctor(s)

        public UnknownCustomerCredentialsPopupComponent(Router router, InternalStore store, SigninFlow signinFlow)
        {
            Router = router;
            Store = store;
            SigninFlow = signinFlow;
        }

        #endregion

        #region overrides

        protected override Task Configure(bool state)
        {
            OwnPresenter?.Configure(HandleOutsideAreaTapped, HandleGoToHome, HandleSignIn);
            return Task.CompletedTask;
        }

        protected override Presenter CreatePresenter()
        {
            return new UnknownCustomerCredentialsOverlayPresenter();
        }

        protected override Task Initialize(bool state)
        {
            return Task.CompletedTask;
        }

        protected override Task PresentInternal()
        {
            OwnPresenter?.Present();
            return Task.CompletedTask;
        }

        #endregion

        #region event handlers

        private void HandleOutsideAreaTapped() { }

        private async void HandleGoToHome()
        {
            await MainThread.InvokeOnMainThreadAsync(() => OwnPresenter?.Update(false, true));

            _ = Store.Dispatch(nameof(UpdateTabsUi));

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                OwnPresenter?.Update(false, false);
                await OwnPresenter?.Dismiss();
            });

            await Store.Dispatch(nameof(Signout));
            CompletionSource?.TrySetResult(new ComponentResult(ActionType.GoToHome));

            // var state = new SearchComponent.ComponentState(false);
            // await MainThread.InvokeOnMainThreadAsync(async () => await (SharedRouter.RootComponent as SharedRootComponent).TryAndSelectTabComponent<SearchComponent, SearchComponent.ComponentState>(state));
            await MainThread.InvokeOnMainThreadAsync(() => Router.PresentComponent<SharedRootComponent, bool, bool>(true));
        }

        private async void HandleSignIn()
        {
            await MainThread.InvokeOnMainThreadAsync(() => OwnPresenter?.Update(true, false));

            await Store.Dispatch(nameof(Signout));

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                OwnPresenter?.Update(false, false);
                await OwnPresenter?.Dismiss();
            });
            CompletionSource?.TrySetResult(new ComponentResult(ActionType.SignIn));

            await SigninFlow.HandleSigninFlow(true);
            await Store.Dispatch(nameof(UpdateTabsUi));
        }

        #endregion
    }
}
