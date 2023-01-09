using System;
using System.Threading.Tasks;
using MobileAppForms.Exceptions;
using MobileAppForms.Model;
using MobileAppForms.Model.Core;
using MobileAppForms.Provider.Core;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace MobileAppForms.Provider
{
    public class UtilizerProviderPreferences : UtilizerProvider
    {
        #region const

        private const string AnonymousKey = "Anonymous";
        private const string CustomerCredentialsKey = "CustomerCredentials";

        private const string CustomerKeyPrefix = "Customer*";

        #endregion

        #region auto-properties

        private ConfigurationProvider ConfigurationProvider { get; }

        #endregion

        #region ctor(s)

        public UtilizerProviderPreferences(ConfigurationProvider configurationProvider)
        {
            ConfigurationProvider = configurationProvider;
        }

        #endregion

        #region UtilizerProvider implementation

        public async Task<Anonymous> GetAnonymous()
        {
            var anonymous = await GetDefaultAnonymous();
            var json = Preferences.Get(AnonymousKey, null);
            if (!String.IsNullOrEmpty(json))
            {
                anonymous = JsonConvert.DeserializeObject<Anonymous>(json);
            }

            return anonymous;
        }

        public Customer? GetCustomer(string id)
        {
            Customer? customer = null;

            var key = $"{CustomerKeyPrefix}{id}";
            var json = Preferences.Get(key, null);
            if (!String.IsNullOrEmpty(json))
            {
                customer = JsonConvert.DeserializeObject<Customer>(json);
            }

            return customer;
        }

        public async Task<CustomerCredentials?> GetCustomerCredentials()
        {
            CustomerCredentials? credentials = null;

            try
            {
                var json = await SecureStorage.GetAsync(CustomerCredentialsKey);
                if (!String.IsNullOrEmpty(json))
                {
                    credentials = JsonConvert.DeserializeObject<CustomerCredentials>(json);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return credentials;
        }

        public async Task SetCustomerCredentials(CustomerCredentials credentials)
        {
            try
            {
                var json = JsonConvert.SerializeObject(credentials);
                await SecureStorage.SetAsync(CustomerCredentialsKey, json);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

        }

        public Task ClearCustomerCredentials()
        {
            SecureStorage.Remove(CustomerCredentialsKey);

            return Task.CompletedTask;
        }

        public void SetUtilizer(Utilizer utilizer, string id = null)
        {
            if (utilizer is Customer customer)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    SetCustomer(id, customer);
                }
                else
                {
                    throw new InsufficientCustomerDataException("SetCustomer need a customer Id");
                }
            }
            else if (utilizer is Anonymous anonymous)
            {
                SetAnonymous(anonymous);
            }
        }

        #endregion

        #region helper methods

        private Task<Anonymous> GetDefaultAnonymous()
        {
            return Task.FromResult(new Anonymous());
        }

        private void SetAnonymous(Anonymous anonymous)
        {
            var json = JsonConvert.SerializeObject(anonymous);
            Preferences.Set(AnonymousKey, json);
        }

        private void SetCustomer(string id, Customer customer)
        {
            var key = $"{CustomerKeyPrefix}{id}";
            var json = JsonConvert.SerializeObject(customer);
            Preferences.Set(key, json);
        }

        #endregion
    }
}
