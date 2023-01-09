using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using static Android.Content.Res.Resources;

namespace MobileAppForms.Droid
{
    [Activity(Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        internal static SplashActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var flags = WindowManagerFlags.LayoutNoLimits | WindowManagerFlags.LayoutInScreen | WindowManagerFlags.KeepScreenOn;
            ActivityUtility.ActuateSystemBarsAspect(Window, flags);

            if (Instance is null) Instance = this;

            Intent intent = new Intent(Instance, typeof(ConfigurationActivity));
            intent.AddFlags(ActivityFlags.NoAnimation);
            intent.SetFlags(ActivityFlags.ReorderToFront);
            this.Window.TransitionBackgroundFadeDuration = 0;
            StartActivityIfNeeded(intent, 1);
            Finish();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        public override void OnBackPressed() { }

    }
}