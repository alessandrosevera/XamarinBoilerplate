using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MobileAppForms.Model;
using MobileAppForms.Provider.Core;
using Newtonsoft.Json;

namespace MobileAppForms.Provider
{
    public class ConfigurationProviderLocalFile : ConfigurationProvider
    {
        #region ConfigurationProvider implementation

        public virtual Task<Configuration> GetAppConfiguration()
        {
            Configuration configuration = default(Configuration);

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MobileAppForms.Provider.Configuration.configuration.json"))
            using (StreamReader reader = new StreamReader(stream))
            {
                string ser = reader.ReadToEnd();

                configuration = JsonConvert.DeserializeObject<Configuration>(ser);
            }


            return Task.FromResult(configuration);
        }

        #endregion
    }
}
