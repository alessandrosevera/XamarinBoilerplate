using System;
using UIKit;
using Xamarin.Forms;

namespace MobileAppForms.Xform
{
    public static class NativeXformExtension
    {
        public static UIEdgeInsets ToUI(this Thickness thickness)
        {
            try
            {
                return new UIEdgeInsets((nfloat)thickness.Top, (nfloat)thickness.Left, (nfloat)thickness.Bottom, (nfloat)thickness.Right);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return UIEdgeInsets.Zero;
        }
    }
}
