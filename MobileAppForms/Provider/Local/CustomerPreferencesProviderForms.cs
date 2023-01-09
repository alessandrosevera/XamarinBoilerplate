using System;
using MobileAppForms.Model;
using MobileAppForms.Provider.Core;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace MobileAppForms.Provider
{
    public class CustomerPreferencesProviderForms : CustomerPreferencesProvider
    {

        #region const

        private const string CustomerPreferencesKeyPrefix = "Preferences*";

        #endregion

        #region ctor(s)

        public CustomerPreferencesProviderForms()
        {
        }

        #endregion

        #region CustomerPreferencesProvider implementation

        public CustomerPreferences GetCustomerPreferences(string customerId)
        {
            return GetPreferences<CustomerPreferences>(CustomerPreferencesKeyPrefix, customerId);
        }

        public void SetCustomerPreferences(string customerId, CustomerPreferences customerPreferences)
        {
            var key = $"{CustomerPreferencesKeyPrefix}{customerId}";
            var json = JsonConvert.SerializeObject(customerPreferences);
            Preferences.Set(key, json);
        }

        public void ClearCustomerPreferences(string customerId)
        {
            var key = $"{CustomerPreferencesKeyPrefix}{customerId}";
            Preferences.Remove(key);
        }

        #endregion

        #region helper methods

        private TPref GetPreferences<TPref>(string keyPrefix, string customerId)
        {
            TPref preferences = default(TPref);

            var key = $"{keyPrefix}{customerId}";
            var json = Preferences.Get(key, null);
            if (!String.IsNullOrEmpty(json))
            {
                preferences = JsonConvert.DeserializeObject<TPref>(json);
            }
            return preferences;
        }

        #endregion
    }
}