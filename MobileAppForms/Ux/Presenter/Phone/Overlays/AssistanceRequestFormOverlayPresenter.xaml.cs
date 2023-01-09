using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NGettext;
using VueSharp.Abstraction;
using VueSharp.Model.Core;
using Xamarin.Forms;
using static MobileAppForms.Vue.AssistanceRequestFormPopupComponent;

namespace MobileAppForms.Ux.Phone
{
    public partial class AssistanceRequestFormOverlayPresenter : KeyboardAdaptiveLayout, OverlayPresenter, LocalizableElement
    {
        #region auto-properties

        private Action<AssistanceFormData> ActOnSend { get; set; }
        private Action ActOnOutsideAreaTapped { get; set; }

        Action OverlayPresenter.ActOnTappedOutside { get; set; }

        #endregion

        #region properties

        public AbsoluteLayout AbsoluteRoot => Root;
        public AbsoluteLayout AbsoluteContent => Root;
        internal View Backdrop => UiBackdrop;

        protected override VisualElement MovableElement => FormContainer;

        #endregion

        #region ctor(s)

        public AssistanceRequestFormOverlayPresenter()
        {
            InitializeComponent();
        }

        #endregion

        #region access methods

        internal void Configure(Action actOnOutsideAreaTapped, Action<AssistanceFormData> actOnSend)
        {
            ActOnOutsideAreaTapped = actOnOutsideAreaTapped;
            ActOnSend = actOnSend;
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
            FormTitle.Text = catalog.GetString("Compila il form");
            FullNameEntry.Placeholder = catalog.GetString("Nome e cognome");
            EmailEntry.Placeholder = catalog.GetString("Email");
            TelephoneEntry.Placeholder = catalog.GetString("Telefono");
            MessageEditor.Placeholder = catalog.GetString("Il tuo messaggio");
            SendButton.ButtonText = catalog.GetString("Invia").ToUpper();
        }

        #endregion

        #region event handlers

        private void HandleSendButtonClicked(System.Object sender, System.EventArgs e)
        {
            var formData = new AssistanceFormData(FullNameEntry.Text, EmailEntry.Text, TelephoneEntry.Text, MessageEditor.Text);
            if (!(ActOnSend is null)) ActOnSend(formData);
        }

        private void HandleOutsideAreaTapped(System.Object sender, System.EventArgs e)
        {
            if (!(ActOnOutsideAreaTapped is null)) ActOnOutsideAreaTapped();
        }

        private void HandleFullNameEntryFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                // Email.SetError(false);
                TryAndActuateMakeRoomForKeyboard(EmailEntry);
            }
            else
            {
                // bool isValid = ValidateEmail();
                // Email.SetError(!isValid);
            }
        }

        private void HandleLastNameEntryFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                // Email.SetError(false);
                TryAndActuateMakeRoomForKeyboard(EmailEntry);
            }
            else
            {
                // bool isValid = ValidateEmail();
                // Email.SetError(!isValid);
            }
        }

        private void HandleEmailEntryFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                // Email.SetError(false);
                TryAndActuateMakeRoomForKeyboard(TelephoneEntry);
            }
            else
            {
                // bool isValid = ValidateEmail();
                // Email.SetError(!isValid);
            }
        }

        private void HandleTelephoneEntryFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                // Email.SetError(false);
                TryAndActuateMakeRoomForKeyboard(SendButton);
            }
            else
            {
                // bool isValid = ValidateEmail();
                // Email.SetError(!isValid);
            }
        }

        private void HandleInactiveAreaTapped(System.Object sender, System.EventArgs e) { }

        #endregion
    }
}
