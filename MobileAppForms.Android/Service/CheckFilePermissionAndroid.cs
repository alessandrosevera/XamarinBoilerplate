using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using MobileAppForms.Droid;
using MobileAppForms.Model;
using MobileAppForms.Service.Core;

namespace MobileAppForms.Service
{
    public class CheckFilePermissionAndroid : CheckFilePermission
    {
        #region auto-properties

        private Activity Activity { get; }
        private TaskCompletionSource<RequestPermissionResult> TaskCompletionSource { get; set; }

        #endregion


        #region ctor(s)

        public CheckFilePermissionAndroid(Activity activity)
        {
            Activity = activity;
        }

        #endregion


        #region CheckFilePermission implementation

        public bool CheckExternalStoragePermission()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                return ((Android.Support.V4.Content.ContextCompat.CheckSelfPermission(Activity, Android.Manifest.Permission.WriteExternalStorage) == (int)Permission.Granted) && (Android.Support.V4.Content.ContextCompat.CheckSelfPermission(Activity, Android.Manifest.Permission.ReadExternalStorage) == (int)Permission.Granted));
            }

            return true;
        }

        public Task<RequestPermissionResult> RequestExternalStoragePermission()
        {
            TaskCompletionSource = new TaskCompletionSource<RequestPermissionResult>();

            if (!CheckExternalStoragePermission())
            {
                var permissions = new string[] { Android.Manifest.Permission.ReadExternalStorage, Android.Manifest.Permission.WriteExternalStorage };
                Android.Support.V4.App.ActivityCompat.RequestPermissions(Activity, permissions, MainActivity.EXTERNAL_STORAGE_PERMISSION_CODE);
            }
            else
            {
                TaskCompletionSource.TrySetResult(RequestPermissionResult.Granted);
            }

            return TaskCompletionSource.Task;
        }

        public void OnRequestPermissionsResult(RequestPermissionResult permissionRequestResult)
        {
            TaskCompletionSource.TrySetResult(permissionRequestResult);
        }

        #endregion
    }
}
