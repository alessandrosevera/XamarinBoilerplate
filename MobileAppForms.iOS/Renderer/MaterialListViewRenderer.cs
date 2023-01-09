using System;
using MobileAppForms.iOS;
using MobileAppForms.Ux.Controls;
using MobileAppForms.Xform;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MaterialListView), typeof(MaterialListViewRenderer))]
namespace MobileAppForms.iOS
{
    public class MaterialListViewRenderer : ListViewRenderer
    {
        #region access methods

        /// <summary>
        /// This method ensures that we don't get stripped out by the linker.
        /// </summary>
        public static void Initialize()
        {
#pragma warning disable 0219
            var ignore1 = typeof(MaterialListViewRenderer);
            var ignore2 = typeof(MaterialListView);
#pragma warning restore 0219
        }

        #endregion

        #region overrides

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (!(e.NewElement is null) && e.NewElement is MaterialListView materialListView)
            {
                Control.ContentInset = materialListView.ContentInset.ToUI();
            }
        }

        #endregion
    }
}
