using System;
using System.Threading.Tasks;
using MobileAppForms.Model;
using MobileAppForms.Provider.Core;
using VueSharp;
using ProfileContext = VuexSharp.ActionContext<MobileAppForms.Store.ProfileState, MobileAppForms.Store.InternalState>;

namespace MobileAppForms.Store
{
    public class Signout : VuexSharp.Action<ProfileState, InternalState>
    {
        #region auto-properties

        private UtilizerProvider UtilizerProvider { get; }
        private Router Router { get; }

        #endregion

        #region ctor(s)

        public Signout(UtilizerProvider utilizerProvider, Router router)
        {
            UtilizerProvider = utilizerProvider;
            Router = router;
        }

        #endregion

        #region VuexSharp.Action implementation

        public async Task Execute(ProfileContext context, object payload)
        {
            bool didCallApi = payload is bool && (bool)payload;

            /*
            if (didCallApi)
            {
                await UserProvider.Logout();
            }
            */

            await UtilizerProvider.ClearCustomerCredentials();

            if (context.State.CurrentUtilizer is Customer customer)
            {
                var downgradedClone = customer.ToAnonymous();
                UtilizerProvider.SetUtilizer(downgradedClone);
            }

            var anonymous = await UtilizerProvider.GetAnonymous();

            context.Commit(nameof(SetCurrentUtilizer), anonymous);
            context.Commit(nameof(SetCurrentCustomerCredentials), null);

            await Router.UnpresentRootComponent();
        }

        #endregion
    }
}
