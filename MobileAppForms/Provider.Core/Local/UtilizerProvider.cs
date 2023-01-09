using System;
using System.Threading.Tasks;
using MobileAppForms.Model;
using MobileAppForms.Model.Core;

namespace MobileAppForms.Provider.Core
{
    public interface UtilizerProvider
    {
        Task<Anonymous> GetAnonymous();
        Customer? GetCustomer(string id);

        Task<CustomerCredentials?> GetCustomerCredentials();
        Task SetCustomerCredentials(CustomerCredentials credentials);
        Task ClearCustomerCredentials();

        void SetUtilizer(Utilizer utilizer, string id = null);

    }
}
