using System;
using Foundation;
using MobileAppForms.Service.Core;

namespace MobileAppForms.Service
{
    public class BaseUrlIos : BaseUrl
    {
        #region BaseUrl implementation

        public string Get()
        {
            return NSBundle.MainBundle.BundlePath;
        }

        #endregion
    }
}
