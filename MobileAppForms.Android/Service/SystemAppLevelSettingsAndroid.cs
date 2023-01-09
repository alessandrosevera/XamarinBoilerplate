using System;
using MobileAppForms.Droid;
using MobileAppForms.Service.Core;

namespace MobileAppForms.Service
{
    public class SystemAppLevelSettingsAndroid : SystemAppLevelSettings
    {
        #region SystemAppLevelSettings implementation

        public void Open()
        {
            // MainActivity.Instance.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionLocationSourceSettings));
            MainActivity.Instance.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionApplicationDetailsSettings, Android.Net.Uri.Parse("package:" + Android.App.Application.Context.PackageName)));
        }

        #endregion
    }
}
