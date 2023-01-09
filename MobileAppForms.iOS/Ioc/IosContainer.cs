using System;
using Autofac;
using MobileAppForms.Service;
using MobileAppForms.Service.Core;
using MobileAppForms.iOS;
using VueSharp.Ioc;
using VueSharp.Service.Core;

namespace MobileAppForms.Ioc
{
    public class IosContainer : SharedContainer
    {
        protected override void BuildInternal(ContainerBuilder builder)
        {
            base.BuildInternal(builder);

            _ = builder.RegisterType<KeyboardListenerIos>().As<KeyboardListener>();
            _ = builder.RegisterType<DeviceIdentifierIos>().As<DeviceIdentifier>();
            _ = builder.RegisterType<SafeAreaInsetsServiceIos>().As<SafeAreaInsetsService>();
            _ = builder.RegisterType<SystemAppLevelSettingsIos>().As<SystemAppLevelSettings>();
            _ = builder.RegisterType<BaseUrlIos>().As<BaseUrl>();
            _ = builder.RegisterType<DbFileHelperIos>().As<DbFileHelper>();
            _ = builder.RegisterType<CheckFilePermissionIos>().As<CheckFilePermission>();
        }
    }
}
