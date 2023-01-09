using System;
using Xamarin.Forms;

namespace MobileAppForms.Xform
{
    public static class NativeXformExtension
    {
        public static (int left, int top, int right, int bottom) ToDroid(this Thickness thickness)
        {
            try
            {
                int left = Convert.ToInt32(thickness.Left);
                int top = Convert.ToInt32(thickness.Top);
                int right = Convert.ToInt32(thickness.Right);
                int bottom = Convert.ToInt32(thickness.Bottom);

                return (left, top, right, bottom);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return (0, 0, 0, 0);
        }
    }
}
