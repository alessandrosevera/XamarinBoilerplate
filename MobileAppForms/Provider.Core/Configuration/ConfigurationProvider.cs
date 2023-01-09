using System;
using System.Threading.Tasks;
using MobileAppForms.Model;

namespace MobileAppForms.Provider.Core
{
    public interface ConfigurationProvider
    {
        Task<Configuration> GetAppConfiguration();
    }
}
