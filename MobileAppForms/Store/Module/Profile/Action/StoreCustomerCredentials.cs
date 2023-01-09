using System;
using System.Threading.Tasks;
using MobileAppForms.Model;
using MobileAppForms.Provider.Core;
using ProfileContext = VuexSharp.ActionContext<MobileAppForms.Store.ProfileState, MobileAppForms.Store.InternalState>;

namespace MobileAppForms.Store
{
    public class StoreCustomerCredentials : VuexSharp.Action<ProfileState, InternalState>
    {
        #region auto-properties

        private UtilizerProvider UtilizerProvider { get; }

        #endregion

        #region ctor(s)

        public StoreCustomerCredentials(UtilizerProvider utilizerProvider)
        {
            UtilizerProvider = utilizerProvider;
        }

        #endregion

        #region VuexSharp.Action implementation

        public Task Execute(ProfileContext context, object payload)
        {
            var newCredentials = payload as CustomerCredentials?;

            if (newCredentials.HasValue)
            {
                System.Diagnostics.Debug.WriteLine(DateTime.Now + "  $$$$$$$$$$ Storing Credentials Tokens Are " + newCredentials.Value.BearerToken);

                context.Commit(nameof(SetCurrentCustomerCredentials), newCredentials.Value);
                UtilizerProvider.SetCustomerCredentials(newCredentials.Value);
            }

            return Task.CompletedTask;
        }



        #endregion
    }
}
