using System;
using System.Threading.Tasks;
using MobileAppForms.Exceptions;
using MobileAppForms.Provider.Core;
using ProfileContext = VuexSharp.ActionContext<MobileAppForms.Store.ProfileState, MobileAppForms.Store.InternalState>;

namespace MobileAppForms.Store
{
    public class LoadCustomerPreferences : VuexSharp.Action<ProfileState, InternalState>
    {
        #region auto-properties

        private UtilizerProvider UtilizerProvider { get; }
        private CustomerPreferencesProvider CustomerPreferencesProvider { get; }

        #endregion

        #region ctor(s)

        public LoadCustomerPreferences(UtilizerProvider utilizerProvider,
            CustomerPreferencesProvider customerPreferencesProvider)
        {
            UtilizerProvider = utilizerProvider;
            CustomerPreferencesProvider = customerPreferencesProvider;
        }

        #endregion

        #region VuexSharp.Action implementation

        public Task Execute(ProfileContext context, object payload)
        {
            var customerCredentials = context.State.CurrentCustomerCredentials;

            if (!customerCredentials.HasValue)
            {
                throw new UnknownCustomerCredentialsException();
            }
            else
            {
                var preferences = CustomerPreferencesProvider.GetCustomerPreferences(customerCredentials.Value.CustomerId);
                context.Commit(nameof(SetCurrentCustomerPreferences), preferences);
            }

            return Task.CompletedTask;
        }


        #endregion
    }
}
