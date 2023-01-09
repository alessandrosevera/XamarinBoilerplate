using System;
using MobileAppForms.Service.Core;

namespace MobileAppForms.Service
{
    public class BaseUrlAndroid : BaseUrl
    {

        #region BaseUrl implementation

        public string Get()
        {
            return "file:///android_asset/";
        }

        #endregion
    }
}
