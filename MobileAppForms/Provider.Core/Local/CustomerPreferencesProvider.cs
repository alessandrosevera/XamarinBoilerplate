using System;
using MobileAppForms.Model;

namespace MobileAppForms.Provider.Core
{
    public interface CustomerPreferencesProvider
    {
        public void SetCustomerPreferences(string customerId, CustomerPreferences customerPreferences);
        public void ClearCustomerPreferences(string customerId);
        public CustomerPreferences GetCustomerPreferences(string customerId);
    }
}
