using System;
using System.Threading.Tasks;
using MobileAppForms.Model;

namespace MobileAppForms.Service.Core
{
    public interface CheckFilePermission
    {
        bool CheckExternalStoragePermission();
        Task<RequestPermissionResult> RequestExternalStoragePermission();
        void OnRequestPermissionsResult(RequestPermissionResult permissionRequestResult);
    }
}

