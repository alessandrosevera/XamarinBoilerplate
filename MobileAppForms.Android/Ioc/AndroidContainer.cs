using System;
using Android.App;
using Autofac;
using MobileAppForms.Listener;
using MobileAppForms.Service;
using MobileAppForms.Service.Core;
using VueSharp.Service.Core;

namespace MobileAppForms.Ioc
{
    public class AndroidContainer : SharedContainer
    {
        #region auto-properties

        private Activity Activity { get; }

        #endregion


        #region ctor(s)

        public AndroidContainer(Activity activity)
        {
            Activity = activity;
        }

        #endregion


        #region overrides

        protected override void BuildInternal(ContainerBuilder builder)
        {
            base.BuildInternal(builder);

            var keyboardListener = new KeyboardListenerAndroid(Activity);

            _ = builder.RegisterInstance(keyboardListener).As<KeyboardListener>();

            _ = builder.RegisterType<DeviceIdentifierAndroid>().As<DeviceIdentifier>();
            _ = builder.RegisterType<SafeAreaInsetsServiceAndroid>().As<SafeAreaInsetsService>();
            _ = builder.RegisterType<SystemAppLevelSettingsAndroid>().As<SystemAppLevelSettings>();
            _ = builder.RegisterType<BaseUrlAndroid>().As<BaseUrl>().SingleInstance();

            var dbFileHelper = new DbFileHelperAndroid(Activity);
            _ = builder.RegisterInstance(dbFileHelper).As<DbFileHelper>();

            var checkFilePermission = new CheckFilePermissionAndroid(Activity);
            _ = builder.RegisterInstance(checkFilePermission).As<CheckFilePermission>();
        }

        #endregion
    }
}
