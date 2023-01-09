using System;
using Android.Content;
using Android.Views;
using MobileAppForms.Droid;
using MobileAppForms.Ux.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MaterialScrollView), typeof(MaterialScrollViewRenderer))]
namespace MobileAppForms.Droid
{
    public class MaterialScrollViewRenderer : ScrollViewRenderer
    {
		#region properties

		MaterialScrollView CustomElement;

		#endregion


		#region ctor(s)

		public MaterialScrollViewRenderer(Context context) : base(context)
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

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

			if (!(e.NewElement is null) && e.NewElement is MaterialScrollView materialScrollView)
            {
				CustomElement = materialScrollView;
            }
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            bool isScrollEnabled = !(CustomElement is null) ? CustomElement.IsScrollEnabled : true;
            switch (ev.Action)
            {
                case MotionEventActions.Down:
                    return isScrollEnabled && base.OnTouchEvent(ev);
                default:
                    return base.OnTouchEvent(ev);
            }
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            bool isScrollEnabled = !(CustomElement is null) ? CustomElement.IsScrollEnabled : true;
            return isScrollEnabled && base.OnInterceptTouchEvent(ev);
        }

        #endregion

    }
}
