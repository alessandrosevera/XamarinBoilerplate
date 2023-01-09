using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NGettext;
using VueSharp.Abstraction;
using VueSharp.Model.Core;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Phone
{
    public partial class AuthenticationRequiredPopupPresenter : AbsoluteLayout, OverlayPresenter, LocalizableElement
    {
        #region fields

        private string _requestedTab;
        private string _localizedText;

        #endregion

        #region auto-properties

        private Action ActOnOutsideAreaTapped { get; set; }
        private Action ActOnSignInClicked { get; set; }

        Action OverlayPresenter.ActOnTappedOutside { get; set; }

        #endregion

        #region properties

        public AbsoluteLayout AbsoluteRoot => Root;
        public AbsoluteLayout AbsoluteContent => Root;
        internal View Backdrop => UiBackdrop;

        #endregion

        #region ctor(s)

        public AuthenticationRequiredPopupPresenter()
        {
            InitializeComponent();
        }

        #endregion

        #region access methods

        internal void Configure(Action actOnOutsideAreaTapped, Action actOnSignInClicked)
        {
            ActOnOutsideAreaTapped = actOnOutsideAreaTapped;
            ActOnSignInClicked = actOnSignInClicked;
        }

        internal void Initialize(string requestedTab)
        {
            _requestedTab = requestedTab;

            if (!string.IsNullOrEmpty(_localizedText) && !string.IsNullOrEmpty(_requestedTab))
            {
                PopupText.HtmlText = string.Format(_localizedText, _requestedTab);
            }
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

        #endregion

        #region LocalizableElement implementation

        public void ApplyLocalization(ICatalog catalog)
        {
            PopupTitle.Text = catalog.GetString("Accesso necessario");
            _localizedText = catalog.GetString("Per visualizzare la sezione <b>{0}</b> devi prima effettuare l'accesso", _requestedTab);
            SignIn.ButtonText = catalog.GetString("Accedi").ToUpper();

            if (!string.IsNullOrEmpty(_requestedTab))
            {
                PopupText.HtmlText = string.Format(_localizedText, _requestedTab);
            }
        }

        #endregion

        #region event handlers

        private void HandleInactiveAreaTapped(System.Object sender, System.EventArgs e) { }

        private void HandleOutsideAreaTapped(System.Object sender, System.EventArgs e)
        {
            if (!(ActOnOutsideAreaTapped is null)) ActOnOutsideAreaTapped();
        }

        private void HandleSignInClicked(System.Object sender, System.EventArgs e)
        {
            if (!(ActOnSignInClicked is null)) ActOnSignInClicked();
        }

        #endregion
    }
}
