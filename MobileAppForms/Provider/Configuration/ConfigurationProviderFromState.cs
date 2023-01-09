using System;
using System.Threading.Tasks;
using MobileAppForms.Model;
using MobileAppForms.Store;

namespace MobileAppForms.Provider
{
    public class ConfigurationProviderFromState : ConfigurationProviderLocalFile
    {
        #region auto-properties

        private InternalStore Store { get; }

        #endregion

        #region ctor(s)

        public ConfigurationProviderFromState(InternalStore store)
        {
            Store = store;
        }

        #endregion

        #region ConfigurationProvider implementation

        public override async Task<Configuration> GetAppConfiguration()
        {
            try
            {
                if (Store.AppContextModule.State.Configuration.HasValue) return Store.AppContextModule.State.Configuration.Value;
                else return await base.GetAppConfiguration();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                /*var errorResponse = await ex.GetErrorResponse();
                if (!String.IsNullOrEmpty(errorResponse))
                {
                    System.Diagnostics.Debug.WriteLine(errorResponse);
                }*/

                return await GetFallbackConfiguration();
            }
        }

        #endregion

        #region helper methods

        protected async Task<Configuration> GetFallbackConfiguration()
        {
            return await base.GetAppConfiguration();
        }

        #endregion
    }
}
