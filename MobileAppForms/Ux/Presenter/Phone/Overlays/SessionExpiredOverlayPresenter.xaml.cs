using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NGettext;
using VueSharp.Abstraction;
using VueSharp.Model.Core;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Phone
{
    public partial class SessionExpiredOverlayPresenter : AbsoluteLayout, OverlayPresenter, LocalizableElement
    {
        #region auto-properties

        private Action ActOnOutsideAreaTapped { get; set; }
        private Action ActOnLeave { get; set; }
        private Action ActOnSignIn { get; set; }

        Action OverlayPresenter.ActOnTappedOutside { get; set; }

        #endregion

        #region properties

        public AbsoluteLayout AbsoluteRoot => Root;
        public AbsoluteLayout AbsoluteContent => Root;
        internal View Backdrop => UiBackdrop;

        #endregion

        #region ctor(s)

        public SessionExpiredOverlayPresenter()
        {
            InitializeComponent();
        }

        #endregion

        #region access methods

        internal void Configure(Action actOnOutsideAreaTapped, Action actOnLeave, Action actOnSignIn)
        {
            ActOnOutsideAreaTapped = actOnOutsideAreaTapped;
            ActOnLeave = actOnLeave;
            ActOnSignIn = actOnSignIn;
        }

        internal async Task Present()
        {
            _ = UiBackdrop.Show();
            await Task.Delay(150);

            _ = FormContainer.FadeTo(1, 75);
        }

        internal async Task Dismiss()
        {
            await FormContainer.FadeTo(0, 150);
            _ = UiBackdrop.Hide();

            await Task.Delay(150);
        }

        internal void Update(bool signinIsActing, bool goToHomeIsActing)
        {
            SignInButton.IsLoading = signinIsActing;
            LeaveButton.IsLoading = goToHomeIsActing;
            FormContainer.IsEnabled = !signinIsActing && !goToHomeIsActing;
        }

        #endregion

        #region LocalizableElement implementation

        public void ApplyLocalization(ICatalog catalog)
        {
            PopupTitle.Text = catalog.GetString("Sessione scaduta");
            PopupText.HtmlText = catalog.GetString("Sessione utente scaduta, per favore <b>ripeti la login</b> o <b>torna alla home</b>.");
            LeaveButton.ButtonText = catalog.GetString("Abbandona").ToUpper();
            SignInButton.ButtonText = catalog.GetString("Accedi").ToUpper();
        }

        #endregion

        #region event handlers

        private void HandleLeaveClicked(System.Object sender, System.EventArgs e)
        {
            if (!(ActOnLeave is null)) ActOnLeave();
        }

        private void HandleSignInClicked(System.Object sender, System.EventArgs e)
        {
            if (!(ActOnSignIn is null)) ActOnSignIn();
        }

        private void HandleInactiveAreaTapped(System.Object sender, System.EventArgs e)
        {
        }

        #endregion
    }
}

