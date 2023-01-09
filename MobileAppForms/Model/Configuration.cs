using System;
using System.Collections.Generic;

namespace MobileAppForms.Model
{
    public readonly struct Configuration
    {
        #region auto-properties

        public string ApiRoot { get; }
        public string TwoLetterIsoFallbackLanguageName { get; }
        public string AssistanceEmail { get; }
        // public string[] SupportedLanguages { get; }
        public Dictionary<string, Denomination> SupportedLanguages { get; }
        
        #endregion

        #region ctor(s)

        public Configuration(string apiRoot, string twoLetterIsoFallbackLanguageName,
            string assistanceEmail, Dictionary<string, Denomination> supportedLanguages)
        {
            ApiRoot = apiRoot;
            TwoLetterIsoFallbackLanguageName = twoLetterIsoFallbackLanguageName;
            AssistanceEmail = assistanceEmail;
            SupportedLanguages = supportedLanguages;
        }

        #endregion
    }
}
