using System;
using Android.Content;
using Android.OS;
using Android.Views;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;

namespace MobileAppForms.Droid
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = false, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class ConfigurationActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        #region auto-properties

        private LauncherApp LauncherApp { get; set; }

        #endregion

        #region overrides

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var flags = WindowManagerFlags.LayoutNoLimits | WindowManagerFlags.LayoutInScreen | WindowManagerFlags.KeepScreenOn;
            ActivityUtility.ActuateSystemBarsAspect(Window, flags);

            LauncherApp = new LauncherApp();
            LoadApplication(LauncherApp);

            _ = StartupOperations();
        }

        public override void OnBackPressed() { }

        #endregion

        public async Task StartupOperations()
        {
            var launcherOperation = await LauncherApp.Operate();

            Intent intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("serializedLauncherOperation", JsonConvert.SerializeObject(launcherOperation));
            intent.AddFlags(ActivityFlags.NoAnimation);
            intent.SetFlags(ActivityFlags.ReorderToFront);
            this.Window.TransitionBackgroundFadeDuration = 0;
            StartActivityIfNeeded(intent, 1);
            Finish();
        }


    }
}