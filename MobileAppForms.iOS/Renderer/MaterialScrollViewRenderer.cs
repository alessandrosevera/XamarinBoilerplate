using System;
using MobileAppForms.iOS;
using MobileAppForms.Ux.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MaterialScrollView), typeof(MaterialScrollViewRenderer))]
namespace MobileAppForms.iOS
{
    public class MaterialScrollViewRenderer : ScrollViewRenderer
    {
        #region access methods

        /// <summary>
        /// This method ensures that we don't get stripped out by the linker.
        /// </summary>
        public static void Initialize()
        {
#pragma warning disable 0219
            var ignore1 = typeof(MaterialScrollViewRenderer);
            var ignore2 = typeof(MaterialScrollView);
#pragma warning restore 0219
        }

        #endregion

        #region overrides

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            Bounces = false;

            if (!(e.NewElement is null) && e.NewElement is MaterialScrollView materialScrollView)
            {
                ScrollEnabled = materialScrollView.IsScrollEnabled;
            }
        }

        #endregion
    }
}
