using System;
using System.Threading.Tasks;
using MobileAppForms.Model;
using MobileAppForms.Model.Core;
using MobileAppForms.Provider.Core;
using ProfileContext = VuexSharp.ActionContext<MobileAppForms.Store.ProfileState, MobileAppForms.Store.InternalState>;

namespace MobileAppForms.Store
{
    public class LoadCurrentUtilizer : VuexSharp.Action<ProfileState, InternalState>
    {
        #region auto-properties

        private UtilizerProvider UtilizerProvider { get; }

        #endregion

        #region ctor(s)

        public LoadCurrentUtilizer(UtilizerProvider utilizerProvider)
        {
            UtilizerProvider = utilizerProvider;
        }

        #endregion

        #region VuexSharp.Action implementation

        public async Task Execute(ProfileContext context, object payload)
        {
            Utilizer utilizer;

            var credentials = await UtilizerProvider.GetCustomerCredentials();

            if (!credentials.HasValue)
            {
                //UTENTE ANONIMO
                utilizer = await UtilizerProvider.GetAnonymous();
            }
            else
            {
                var customerId = credentials.Value.CustomerId;
                utilizer = UtilizerProvider.GetCustomer(customerId);

                if (utilizer is null)
                {
                    utilizer = await UtilizerProvider.GetAnonymous();
                    credentials = null;
                }
            }

            context.Commit(nameof(SetCurrentUtilizer), utilizer);
            context.Commit(nameof(SetCurrentCustomerCredentials), credentials);

            if (utilizer is Customer)
            {
                await context.Dispatch(nameof(LoadCustomerPreferences));
            }
        }

        #endregion
    }
}
