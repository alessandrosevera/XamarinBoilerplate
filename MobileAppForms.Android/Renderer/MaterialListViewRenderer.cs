using System;
using Android.Content;
using MobileAppForms.Droid;
using MobileAppForms.Ux.Controls;
using MobileAppForms.Xform;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MaterialListView), typeof(MaterialListViewRenderer))]
namespace MobileAppForms.Droid
{
    public class MaterialListViewRenderer : ListViewRenderer
    {
        #region ctor(s)

        public MaterialListViewRenderer(Context context) : base(context)
        {
        }

        #endregion

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

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (!(e.NewElement is null) && e.NewElement is MaterialListView materialListView)
            {                
                Control.SetClipToPadding(materialListView.ClipToPadding);
                var paddingInset = materialListView.ContentInset.ToDroid();
                Control.SetPadding(paddingInset.left, paddingInset.top, paddingInset.right, paddingInset.bottom);
            }
        }

        #endregion
    }
}
