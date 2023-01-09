using System;
using MobileAppForms.Service.Core;
using Xamarin.Essentials;

namespace MobileAppForms.Service
{
    public class SystemAppLevelSettingsIos : SystemAppLevelSettings
    {
        #region SystemAppLevelSettings implementation

        public void Open()
        {
            Launcher.TryOpenAsync("app-settings:");
        }

        #endregion
    }
}
