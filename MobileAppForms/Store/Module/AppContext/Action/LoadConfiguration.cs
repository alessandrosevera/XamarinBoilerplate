using System.Threading.Tasks;
using MobileAppForms.Provider.Core;
using AppContext = VuexSharp.ActionContext<MobileAppForms.Store.AppContextState, MobileAppForms.Store.InternalState>;

namespace MobileAppForms.Store
{
    public class LoadConfiguration : VuexSharp.Action<AppContextState, InternalState>
    {
        #region auto-properties

        private ConfigurationProvider ConfigurationProvider { get; }

        #endregion

        #region ctor(s)

        public LoadConfiguration(ConfigurationProvider configurationProvider)
        {
            ConfigurationProvider = configurationProvider;
        }

        #endregion

        #region VuexSharp.Action implementation

        public async Task Execute(AppContext context, object payload)
        {
            var configuration = await ConfigurationProvider.GetAppConfiguration();
            context.Commit(nameof(SetConfiguration), configuration);
        }

        #endregion
    }
}
