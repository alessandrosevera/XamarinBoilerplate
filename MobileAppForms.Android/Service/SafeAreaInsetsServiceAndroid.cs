using System;
using MobileAppForms.Droid;
using MobileAppForms.Service.Core;
using VueSharp.Model;
using VueSharp.Service.Core;

namespace MobileAppForms.Service
{
    public class SafeAreaInsetsServiceAndroid : SharedSafeAreaInsetsService, SafeAreaInsetsService
    {
        #region ctor(s)

        public SafeAreaInsetsServiceAndroid(DeviceIdentifier deviceIdentifier) : base(deviceIdentifier)
        {

        }

        #endregion

        #region SafeAreaInsetsInfo implementation

        public SafeAreaInsets GetSafeAreaInsets(bool getCached = false)
        {
            if (getCached)
            {
                var preferenceInsets = GetValue();
                if (preferenceInsets.HasValue) return preferenceInsets.Value;
            }

            var statusBarHeight = MainActivity.Instance.GetStatusBarHeightFromResources() / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
            var bottomSystemBarHeight = MainActivity.Instance.GetSystemBarHeightFromResources() / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
            var insets = new SafeAreaInsets(0, statusBarHeight, 0, bottomSystemBarHeight);
            SetValue(insets);

            return insets;
        }

        #endregion

    }
}
