using System;
using System.Threading.Tasks;
using MobileAppForms.Model;
using MobileAppForms.Service.Core;

namespace MobileAppForms.Service
{
    public class CheckFilePermissionIos : CheckFilePermission
    {
        #region auto-properties

        private TaskCompletionSource<RequestPermissionResult> TaskCompletionSource { get; set; }

        #endregion


        #region ctor(s)

        public CheckFilePermissionIos()
        {
        }

        #endregion


        #region CheckFilePermission implementation

        public bool CheckExternalStoragePermission()
        {
            return true;
        }

        public Task<RequestPermissionResult> RequestExternalStoragePermission()
        {
            TaskCompletionSource = new TaskCompletionSource<RequestPermissionResult>();

            TaskCompletionSource.TrySetResult(RequestPermissionResult.Granted);

            return TaskCompletionSource.Task;
        }

        public void OnRequestPermissionsResult(RequestPermissionResult permissionRequestResult)
        {
            TaskCompletionSource.TrySetResult(permissionRequestResult);
        }

        #endregion
    }
}
