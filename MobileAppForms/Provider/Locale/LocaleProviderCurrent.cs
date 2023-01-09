using System;
using System.Globalization;
using System.Threading.Tasks;
using MobileAppForms.Provider.Core;
using MobileAppForms.Store;
using VueSharp.Provider.Core;

namespace MobileAppForms.Provider
{
    public class LocaleProviderCurrent : LocaleProvider
    {
        #region auto-properties

        private ConfigurationProvider ConfigurationProvider { get; }
        private InternalStore Store { get; }

        private string FallbackLanguageStorage { get; set; }

        #endregion


        #region ctor(s)

        public LocaleProviderCurrent(InternalStore store, ConfigurationProvider configurationProvider)
        {
            Store = store;
            ConfigurationProvider = configurationProvider;
        }

        #endregion

        #region LocaleProvider implementation


        public Task<string> GetTwoLetterIsoLanguageName()
        {
            if (!string.IsNullOrEmpty(Store.AppContextModule.State.CustomTwoLetterIsoLanguageUI))
            {
                return Task.FromResult(Store.AppContextModule.State.CustomTwoLetterIsoLanguageUI.ToUpper());
            }
            return Task.FromResult(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToUpper());
        }

        public Task<string> GetTwoLetterIsoFallbackLanguageName()
        {
            if (FallbackLanguageStorage is null && Store.AppContextModule.State.Configuration.HasValue)
            {
                var configuration = Store.AppContextModule.State.Configuration.Value;
                FallbackLanguageStorage = configuration.TwoLetterIsoFallbackLanguageName.ToUpper();
            }

            return Task.FromResult(FallbackLanguageStorage);
        }


        public Task<string> GetLocaleName()
        {
            return Task.FromResult(CultureInfo.CurrentUICulture.Name);
        }

        #endregion
    }
}
