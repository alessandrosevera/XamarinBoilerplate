using System;
using MobileAppForms.Service;
using MobileAppForms.Store;
using MobileAppForms.Ux;
using MobileAppForms.Vue;
using VueSharp;
using VueSharp.Abstraction;
using VuexSharp.Ioc.Core;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppForms
{
    public partial class App : Application
    {
        #region auto-properties

        private bool WasOnStartCalled { get; set; }
        private bool WasOperateCalled { get; set; }

        private Container Container { get; set; }
        private Model.Configuration? EnrolledConfiguration { get; set; }

        #endregion

        #region ctor(s)

        public App()
        {
            InitializeComponent();
        }

        #endregion

        #region overrides

        protected override void OnStart()
        {
            var store = Container.Resolve<InternalStore>();
            store.InitializeStore();

            if (EnrolledConfiguration.HasValue) store.Commit(nameof(SetConfiguration), EnrolledConfiguration.Value);

            WasOnStartCalled = true;
            ArbitrateStart();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion

        #region access methods

        public void Operate(Container container, Model.Configuration? appConfiguration = null)
        {
            Container = container;
            EnrolledConfiguration = appConfiguration;

            var displayInfo = DeviceDisplay.MainDisplayInfo;
            var height = displayInfo.Height / displayInfo.Density;
            var width = displayInfo.Width / displayInfo.Density;
            ScalableUi.Scale(height, width, DeviceInfo.Platform);

            WasOperateCalled = true;
            ArbitrateStart();
        }

        #endregion

        #region helper methods

        private void ArbitrateStart()
        {
            if (WasOnStartCalled && WasOperateCalled)
            {
                var store = Container.Resolve<InternalStore>();
                if (!store.AppContextModule.State.Configuration.HasValue) MainPage = new PreWorkflowPage();

                var signinFlow = Container.Resolve<SigninFlow>();
                _ = signinFlow.HandleSigninFlow();
            }
        }

        #endregion

        #region event handlers

        public bool OnBackPressed()
        {
            if (WasOperateCalled)
            {
                var router = Container.Resolve<Router>();
                return router.OnDeviceBackPressed();
            }
            return false;
        }

        #endregion
    }
}
