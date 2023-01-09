using System;
using Android.OS;
using Android.Views;
using Xamarin.Forms.Platform.Android;

namespace MobileAppForms.Droid
{
    public static class ActivityUtility
    {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public static void ActuateSystemBarsAspect(Window? window, WindowManagerFlags? flags = null)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            try
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    if (flags.HasValue) window.AddFlags(flags.Value);
                    window.DecorView.SetFitsSystemWindows(true);

                    if ((int)Build.VERSION.SdkInt >= 30) window.SetDecorFitsSystemWindows(false);
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) window.SetNavigationBarColor(Xamarin.Forms.Color.FromHex("#114CA1").ToAndroid());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}

