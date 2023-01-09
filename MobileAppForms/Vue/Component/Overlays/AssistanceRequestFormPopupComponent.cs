using System;
using System.Threading.Tasks;
using MobileAppForms.Model;
using MobileAppForms.Provider.Core;
using MobileAppForms.Service.Core;
using MobileAppForms.Ux.Phone;
using VueSharp;
using VueSharp.Abstraction;
using VueSharp.Provider.Core;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppForms.Vue
{
    public class AssistanceRequestFormPopupComponent : OverlayComponent<AssistanceRequestFormPopupComponent.ComponentState, bool>
    {
        #region nested classes

        public readonly struct ComponentState
        {
            #region auto-properties

            public string Title { get; }
            public string RefId { get; }
            public bool IsGenericAssistanceRequest { get; }

            #endregion

            #region ctor(s)

            public ComponentState(bool isGenericAssistanceRequest, string title, string refId)
            {
                IsGenericAssistanceRequest = isGenericAssistanceRequest;
                Title = title;
                RefId = refId;
            }

            #endregion
        }

        public readonly struct AssistanceFormData
        {
            #region auto-properties

            public string FullName { get; }
            public string Email { get; }
            public string Telephone { get; }
            public string Message { get; }

            #endregion

            #region ctor(s)

            public AssistanceFormData(string fullName, string email, string telephone, string message)
            {
                FullName = fullName;
                Email = email;
                Telephone = telephone;
                Message = message;
            }

            #endregion
        }

        #endregion

        #region auto-properties

        private KeyboardListener KeyboardListener { get; }
        private ConfigurationProvider ConfigurationProvider { get; }
        private CatalogProvider CatalogProvider { get; }
        private Router Router { get; }

        private bool KeyboardIsShown { get; set; }

        #endregion

        #region properties

        public override View Backdrop => (Presenter as AssistanceRequestFormOverlayPresenter)?.Backdrop;
        private AssistanceRequestFormOverlayPresenter OwnPresenter => Presenter as AssistanceRequestFormOverlayPresenter;

        #endregion

        #region ctor(s)

        public AssistanceRequestFormPopupComponent(KeyboardListener keyboardListener, CatalogProvider catalogProvider,
            ConfigurationProvider configurationProvider, Router router)
        {
            KeyboardListener = keyboardListener;
            CatalogProvider = catalogProvider;
            ConfigurationProvider = configurationProvider;
            Router = router;
        }

        #endregion

        #region overrides

        protected override Task Configure(ComponentState state)
        {            
            KeyboardListener?.Configure(HandleKeyboardWillShow, HandleKeyboardWillHide);
            OwnPresenter?.Configure(HandleOutsideAreaTapped, HandleSend);
            return Task.CompletedTask;
        }

        protected override Presenter CreatePresenter()
        {
            return new AssistanceRequestFormOverlayPresenter();
        }

        protected override Task Initialize(ComponentState state)
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

        private async void HandleOutsideAreaTapped()
        {
            await OwnPresenter?.Dismiss();
            CompletionSource?.TrySetResult(false);
        }

        private async void HandleSend(AssistanceFormData formData)
        {
            var catalog = await CatalogProvider.GetLocalCatalog();
            var configuration = await ConfigurationProvider.GetAppConfiguration();
            if (!string.IsNullOrEmpty(configuration.AssistanceEmail))
            {
                try
                {
                    string localizedFullNameLabel = catalog.GetString("Nome e cognome");
                    string localizedEmailLabel = catalog.GetString("Email");
                    string localizedTelephoneLabel = catalog.GetString("Telefono");
                    string localizedMessageLabel = catalog.GetString("Messaggio");
                    string localizedEmptyLabel = catalog.GetString("Non inserito");
                    string localizedRealEstatePropertyReferenceLabel = catalog.GetString("Riferimento immobile");
                    string localizedTitleLabel = catalog.GetString("Titolo");

                    bool hasEmail = (!string.IsNullOrEmpty(formData.Email) && !string.IsNullOrWhiteSpace(formData.Email));
                    bool hasTelephone = (!string.IsNullOrEmpty(formData.Telephone) && !string.IsNullOrWhiteSpace(formData.Telephone));
                    string formEmail = hasEmail ? formData.Email : localizedEmptyLabel;
                    string formTelephone = hasTelephone ? formData.Telephone : localizedEmptyLabel;

                    string emailSubject = string.Empty;

                    if (!State.IsGenericAssistanceRequest)
                    {
                        if (!string.IsNullOrEmpty(State.RefId))
                        {
                            emailSubject = catalog.GetString("Info immobile: {0} - {1}", State.Title, State.RefId);
                        }
                        else
                        {
                            emailSubject = catalog.GetString("Info immobile: {0}", State.Title);
                        }
                    }
                    else
                    {
                        emailSubject = catalog.GetString("Richiesta di assistenza");
                    }

                    string emailBody = "<table>" +
                        "<thead>" +
                        "<th>&nbsp;</th>" +
                        "<th>&nbsp;</th>" +
                        "</thead>" +
                        "<tbody>";

                    if (!State.IsGenericAssistanceRequest)
                    {
                        emailBody += "<tr>" +
                            "<td><b>" + localizedRealEstatePropertyReferenceLabel + "</b></td>" +
                            "<td>" +
                            "<div>" + localizedTitleLabel + ": <b>" + State.Title + "</b></div>";
                        if (!string.IsNullOrEmpty(State.RefId))
                        {
                            emailBody += "<div>ID: <b>" + State.RefId + "</b></div>";
                        }
                        emailBody += "</td></tr>";
                    }

                    emailBody += "<tr>" +
                        "<td>" + localizedFullNameLabel + "</td>" +
                        "<td>" + formData.FullName + "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>" + localizedEmailLabel + "</td>" +
                        "<td>" + (hasEmail ? "<a href=\"mailto:" + formData.Email + "\">" : "") + formEmail + (hasEmail ? "</a>" : "") + "</td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>" + localizedTelephoneLabel + "</td>" +
                        "<td>" + (hasTelephone ? "<a href=\"tel:" + formData.Telephone + "\">" : "") + formEmail + (hasTelephone ? "</a>" : "") + "</td>" +
                        "</tr>";

                    if (!string.IsNullOrEmpty(formData.Message))
                    {
                        emailBody += "<tr>" +
                            "<td>" + localizedMessageLabel + "</td>" +
                            "<td>" + formData.Message + "</td>" +
                            "</tr>";
                    }

                    emailBody += "</tbody>" + "</table>";

                    await Xamarin.Essentials.Email.ComposeAsync(emailSubject, emailBody, new string[] { configuration.AssistanceEmail });
                }
                catch (ArgumentNullException)
                {
                    string errorMessage = catalog.GetString("Email non valida.");
                    var snackbarConfiguration = new SnackbarConfiguration(errorMessage);
                    _ = MainThread.InvokeOnMainThreadAsync(async () => await Router.PresentComponent<ErrorSnackbarComponent, SnackbarConfiguration, bool>(snackbarConfiguration));
                }
                catch (FeatureNotSupportedException)
                {
                    string errorMessage = catalog.GetString("Funzionalità non supportata.");
                    var snackbarConfiguration = new SnackbarConfiguration(errorMessage);
                    _ = MainThread.InvokeOnMainThreadAsync(async () => await Router.PresentComponent<ErrorSnackbarComponent, SnackbarConfiguration, bool>(snackbarConfiguration));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);

                    string errorMessage = catalog.GetString("Si è verificato un errore, riprovare più tardi.");
                    var snackbarConfiguration = new SnackbarConfiguration(errorMessage);
                    _ = MainThread.InvokeOnMainThreadAsync(async () => await Router.PresentComponent<ErrorSnackbarComponent, SnackbarConfiguration, bool>(snackbarConfiguration));
                }
            }
            else
            {
                string errorMessage = catalog.GetString("Email non presente.");
                var snackbarConfiguration = new SnackbarConfiguration(errorMessage);
                _ = MainThread.InvokeOnMainThreadAsync(async () => await Router.PresentComponent<ErrorSnackbarComponent, SnackbarConfiguration, bool>(snackbarConfiguration));
            }

            await OwnPresenter?.Dismiss();
            CompletionSource?.TrySetResult(true);
        }

        private void HandleKeyboardWillShow(KeyboardEventArgs e)
        {
            KeyboardIsShown = true;
#pragma warning disable CS4014 // Non è possibile attendere la chiamata, pertanto l'esecuzione del metodo corrente continuerà prima del completamento della chiamata
            OwnPresenter?.MakeRoomForKeyboard(e.KeyboardFrame.Height, e.KeyboardAnimationDurationMs);
#pragma warning restore CS4014 // Non è possibile attendere la chiamata, pertanto l'esecuzione del metodo corrente continuerà prima del completamento della chiamata
        }

        private void HandleKeyboardWillHide(KeyboardEventArgs e)
        {
            if (KeyboardIsShown)
            {
                KeyboardIsShown = false;
                OwnPresenter?.UnmakeRoomForKeyboard(e.KeyboardAnimationDurationMs);
            }
        }

        #endregion
    }
}
