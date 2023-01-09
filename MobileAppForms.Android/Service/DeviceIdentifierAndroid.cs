using System;
using MobileAppForms.Service.Core;

namespace MobileAppForms.Service
{
    public class DeviceIdentifierAndroid : DeviceIdentifier
    {
        #region DeviceIdentifier implementation

        public string DeviceId =>
                Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);

        #endregion
    }
}
