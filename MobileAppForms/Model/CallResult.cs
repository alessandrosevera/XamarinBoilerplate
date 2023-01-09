using System;
namespace MobileAppForms.Model
{
    public enum CallResult
    {
        Unknown,
        Ok,
        ServerError,
        Unauthorized,
        NoConnection,
        ServerUnreachable,
        Expired,
        Conflict,
        Forbidden,
        NotFound,
        BadRequest,
        Gone
    }
}
