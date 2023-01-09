using System;
using Autofac;
using MobileAppForms.Provider;
using MobileAppForms.Provider.Core;
using MobileAppForms.Service;
using MobileAppForms.Service.Core;
using MobileAppForms.Store;
using VueSharp.Ioc;

namespace MobileAppForms.Ioc
{
    public class SharedContainer : VueContainer
    {
        protected override void BuildInternal(ContainerBuilder builder)
        {
            //PROVIDER
            _ = builder.RegisterType<ConfigurationProviderFromState>().As<ConfigurationProvider>().SingleInstance();
            _ = builder.RegisterType<UtilizerProviderPreferences>().As<UtilizerProvider>().SingleInstance();
            _ = builder.RegisterType<CustomerPreferencesProviderForms>().As<CustomerPreferencesProvider>().SingleInstance();

            //SERVICE
            _ = builder.RegisterType<SigninFlow>().AsSelf().SingleInstance();
            _ = builder.RegisterType<AccessControl>().AsSelf().SingleInstance();

            //STORE
            _ = builder.RegisterType<InternalStore>().AsSelf().SingleInstance();
        }
    }
}
