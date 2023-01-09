using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using MobileAppForms.Ioc;
using SimpleTouchView.Droid;
using SimpleTouchView;
using Newtonsoft.Json;
using System.Linq;
using MobileAppForms.Model;
using MobileAppForms.Service.Core;
using FFImageLoading.Forms.Platform;
using Xamarin.Forms.PancakeView.Droid;

namespace MobileAppForms.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        #region const

        private const int ANDROID_DOCUMENTATION_STATUS_BAR_HEIGHT = 24;
        private const int ANDROID_DOCUMENTATION_BOTTOM_NAVIGATION_BAR_HEIGHT = 48;

        #endregion

        #region auto-properties

        public static int EXTERNAL_STORAGE_PERMISSION_CODE = 101;

        public static AndroidContainer Container { get; private set; }
        internal static MainActivity Instance { get; private set; }
        private App FormsApp { get; set; }

        #endregion

        #region overrides

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            ActivityUtility.ActuateSystemBarsAspect(Window, Android.Views.WindowManagerFlags.KeepScreenOn);

            var xx1 = new TouchViewRenderer(this);
            var xx2 = new TabbedRootPageRenderer(this);

            MaterialScrollViewRenderer.Initialize();
            MaterialListViewRenderer.Initialize();
            BorderlessEntryRenderer.Initialize();
            TouchViewContext.Current.Initialize();

            CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();

            PancakeViewRenderer.Init();

            Instance = this;

            Container = new AndroidContainer(this);
            Container.Build();

            FormsApp = new App();

            Model.Configuration? appConfiguration = null;
            try
            {
                if (Intent.Extras.ContainsKey("serializedLauncherOperation"))
                {
                    var serializedLauncherOperation = Intent.Extras.GetString("serializedLauncherOperation");
                    if (!string.IsNullOrEmpty(serializedLauncherOperation))
                    {
                        var launcherOperation = JsonConvert.DeserializeObject<LauncherOperation>(serializedLauncherOperation);
                        appConfiguration = launcherOperation.AppConfiguration;
                    }
                }
            }
            catch (Exception) { }

            FormsApp.Operate(Container, appConfiguration);

            LoadApplication(FormsApp);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == MainActivity.EXTERNAL_STORAGE_PERMISSION_CODE)
            {
                RequestPermissionResult requestPermissionResult = grantResults.Any(r => r == Permission.Denied) ? RequestPermissionResult.Denied : RequestPermissionResult.Granted;
                Container.Resolve<CheckFilePermission>().OnRequestPermissionsResult(requestPermissionResult);
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (FormsApp is null) return;
            bool managed = FormsApp.OnBackPressed();
            if (!managed) base.OnBackPressed();
        }

        #endregion

        #region access methods

        public int GetStatusBarHeightFromResources()
        {
            var resId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resId > 0) return Resources.GetDimensionPixelSize(resId);
            return ANDROID_DOCUMENTATION_STATUS_BAR_HEIGHT;
        }

        public int GetSystemBarHeightFromResources()
        {
            var resId = Resources.GetIdentifier("navigation_bar_height", "dimen", "android");
            if (resId > 0) return Resources.GetDimensionPixelSize(resId);
            return ANDROID_DOCUMENTATION_BOTTOM_NAVIGATION_BAR_HEIGHT;
        }

        #endregion
    }
}
