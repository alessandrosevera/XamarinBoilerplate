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
    public class SessionExpiredPopupComponent : OverlayComponent<bool, SessionExpiredPopupComponent.ComponentResult>
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

        public override View Backdrop => (Presenter as SessionExpiredOverlayPresenter)?.Backdrop;
        private SessionExpiredOverlayPresenter OwnPresenter => Presenter as SessionExpiredOverlayPresenter;

        private SharedRouter SharedRouter => Router as SharedRouter;

        #endregion

        #region ctor(s)

        public SessionExpiredPopupComponent(Router router, InternalStore store, SigninFlow signinFlow)
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
            return new SessionExpiredOverlayPresenter();
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

            await Store.Dispatch(nameof(Signout));
            _ = Store.Dispatch(nameof(UpdateTabsUi));

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                OwnPresenter?.Update(false, false);
                await OwnPresenter?.Dismiss();
            });
            CompletionSource?.TrySetResult(new ComponentResult(ActionType.GoToHome));

            await MainThread.InvokeOnMainThreadAsync(async () => await (SharedRouter.RootComponent as SharedRootComponent).TryAndSelectTabComponent<SearchComponent, bool>(false));
        }

        private async void HandleSignIn()
        {
            await MainThread.InvokeOnMainThreadAsync(() => OwnPresenter?.Update(true, false));

            await Store.Dispatch(nameof(Signout));
            await SigninFlow.HandleSigninFlow(true);
            await Store.Dispatch(nameof(UpdateTabsUi));

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                OwnPresenter?.Update(false, false);
                await OwnPresenter?.Dismiss();
            });
            CompletionSource?.TrySetResult(new ComponentResult(ActionType.SignIn));
        }

        #endregion
    }
}
