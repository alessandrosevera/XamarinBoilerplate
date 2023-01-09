using System;
using System.Threading.Tasks;
using MobileAppForms.Exceptions;
using MobileAppForms.Model;
using MobileAppForms.Provider.Core;
using ProfileContext = VuexSharp.ActionContext<MobileAppForms.Store.ProfileState, MobileAppForms.Store.InternalState>;

namespace MobileAppForms.Store
{
    public class StoreCurrentCustomer : VuexSharp.Action<ProfileState, InternalState>
    {
        #region auto-properties

        private UtilizerProvider UtilizerProvider { get; }

        #endregion

        #region ctor(s)

        public StoreCurrentCustomer(UtilizerProvider utilizerProvider)
        {
            UtilizerProvider = utilizerProvider;
        }

        #endregion

        #region VuexSharp.Action implementation

        public Task Execute(ProfileContext context, object payload)
        {
            if (context.State.CurrentCustomerCredentials.HasValue)
            {
                var credentials = context.State.CurrentCustomerCredentials.Value;
                if (payload is Customer customer)
                {
                    context.Commit(nameof(SetCurrentUtilizer), customer);
                    UtilizerProvider.SetUtilizer(customer, credentials.CustomerId);
                }
            }
            else
            {
                throw new UnknownCustomerCredentialsException();
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
