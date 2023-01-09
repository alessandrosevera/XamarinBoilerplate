using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobileAppForms.Provider;
using MobileAppForms.Ux;
using Xamarin.Forms;

namespace MobileAppForms
{
    public readonly struct LauncherOperation
    {
        public MobileAppForms.Model.Configuration? AppConfiguration { get; }

        public LauncherOperation(MobileAppForms.Model.Configuration? appConfiguration)
        {
            AppConfiguration = appConfiguration;
        }
    }

    public partial class LauncherApp : Application
    {
        #region ctor(s)

        public LauncherApp()
        {
            InitializeComponent();

            var xx = Application.Current.Resources.Keys;
            MainPage = new PreWorkflowPage();
        }

        #endregion

        #region access methods

        public async Task<LauncherOperation> Operate()
        {
            try
            {
                var configurationProvider = new ConfigurationProviderLocalFile();
                var configuration = await configurationProvider.GetAppConfiguration();
                return new LauncherOperation(configuration);
            }
            catch (Exception)
            {
                return default(LauncherOperation);
            }
        }

        #endregion
    }
}
