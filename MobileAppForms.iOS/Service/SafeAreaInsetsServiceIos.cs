using System;
using MobileAppForms.Service.Core;
using UIKit;
using VueSharp.Model;
using VueSharp.Service.Core;
using Xamarin.Essentials;

namespace MobileAppForms.Service
{
    public class SafeAreaInsetsServiceIos : SharedSafeAreaInsetsService, SafeAreaInsetsService
    {
        #region ctor(s)

        public SafeAreaInsetsServiceIos(DeviceIdentifier deviceIdentifier) : base(deviceIdentifier)
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

            var insetsForms = new SafeAreaInsets(0, 0, 0, 0);
            if (DeviceInfo.Version.Major >= 11)
            {
                UIEdgeInsets UIInsets = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;
                insetsForms = new SafeAreaInsets(UIInsets.Left, UIInsets.Top, UIInsets.Right, UIInsets.Bottom);
            }
            SetValue(insetsForms);

            return insetsForms;
        }

        #endregion
    }
}
