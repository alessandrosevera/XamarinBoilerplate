using System;
using MobileAppForms.Service.Core;
using UIKit;

namespace MobileAppForms.Service
{
    public class DeviceIdentifierIos : DeviceIdentifier
    {
        #region DeviceIdentifier implementation

        public string DeviceId => UIDevice.CurrentDevice.IdentifierForVendor.AsString();

        #endregion
    }
}
