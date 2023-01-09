using System;
using System.Globalization;
using System.Threading.Tasks;
using NGettext;
using VueSharp.Provider.Core;

namespace MobileAppForms.Provider
{
    public class CatalogProviderMoFile : CatalogProvider
    {
        #region const

        private const string LocalCatalogFileTemplateFileName = "MobileAppForms.i18n.MobileAppForms-<language>.mo";

        #endregion

        #region auto-properties

        private LocaleProvider LocaleProvider { get; }
        private ICatalog LocalCatalog { get; set; }

        #endregion

        #region ctor(s)

        public CatalogProviderMoFile(LocaleProvider localeProvider)
        {
            LocaleProvider = localeProvider;
        }

        #endregion

        #region CatalogProvider implementation

        public async Task<string> GetCatalogTwoLetterIsoLanguageName(bool skipFallbackValidation = false)
        {
            var currentUiLanguage = await LocaleProvider.GetTwoLetterIsoLanguageName();
            string currentLanguage = null;

            if (TryAndGetLocalMoFileCatalog(currentUiLanguage) is null)
            {
                var fallbackLanguage = await LocaleProvider.GetTwoLetterIsoFallbackLanguageName();
                if (!(TryAndGetLocalMoFileCatalog(fallbackLanguage) is null) || skipFallbackValidation) currentLanguage = fallbackLanguage;
            }

            if (currentLanguage is null) currentLanguage = currentUiLanguage;

            return currentLanguage;
        }

        public async Task<ICatalog> GetLocalCatalog()
        {
            if (LocalCatalog is null)
            {
                var currentLanguage = await LocaleProvider.GetTwoLetterIsoLanguageName();

                LocalCatalog = TryAndGetLocalMoFileCatalog(currentLanguage);

                if (LocalCatalog is null)
                {
                    var fallbackLanguage = await LocaleProvider.GetTwoLetterIsoFallbackLanguageName();
                    LocalCatalog = TryAndGetLocalMoFileCatalog(fallbackLanguage);
                }
                if (LocalCatalog is null)
                {
                    LocalCatalog = new Catalog();
                }
            }

            return LocalCatalog;
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            LocalCatalog = null;
        }

        #endregion

        #region helper methods

        private ICatalog TryAndGetLocalMoFileCatalog(string language)
        {
            ICatalog catalog = null;
            try
            {
                var catalogFilePath = LocalCatalogFileTemplateFileName.Replace("<language>", language);
                using (var catalogStream = typeof(App).Assembly.GetManifestResourceStream(catalogFilePath))
                {
                    catalog = new Catalog(catalogStream, new CultureInfo(language));
                }

            }
            catch (Exception ex)
            {
                //TODO LOG
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return catalog;
        }


        #endregion
    }
}
