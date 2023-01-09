using System;
using System.Threading.Tasks;
using MobileAppForms.Model;
using MobileAppForms.Provider.Core;
using ProfileContext = VuexSharp.ActionContext<MobileAppForms.Store.ProfileState, MobileAppForms.Store.InternalState>;

namespace MobileAppForms.Store
{
    public class StoreCustomerPreferences : VuexSharp.Action<ProfileState, InternalState>
    {
        #region auto-properties

        private UtilizerProvider UtilizerProvider { get; }
        private CustomerPreferencesProvider CustomerPreferencesProvider { get; }

        #endregion

        #region ctor(s)

        public StoreCustomerPreferences(UtilizerProvider utilizerProvider, CustomerPreferencesProvider customerPreferencesProvider)
        {
            UtilizerProvider = utilizerProvider;
            CustomerPreferencesProvider = customerPreferencesProvider;
        }

        #endregion

        #region VuexSharp.Action implementation

        public Task Execute(ProfileContext context, object payload)
        {
            var customerPreferences = payload as CustomerPreferences?;
            var customer = context.State.CurrentUtilizer as Customer?;
            var credentials = context.State.CurrentCustomerCredentials;

            if (customer.HasValue && credentials.HasValue)
            {
                context.Commit(nameof(SetCurrentCustomerPreferences), customerPreferences);
                CustomerPreferencesProvider.SetCustomerPreferences(credentials.Value.CustomerId, customerPreferences.Value);
            }

            return Task.CompletedTask;
        }



        #endregion
    }
}
