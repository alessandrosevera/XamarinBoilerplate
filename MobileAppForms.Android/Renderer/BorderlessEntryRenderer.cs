using System;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views.InputMethods;
using Android.Widget;
using MobileAppForms.Droid;
using MobileAppForms.Ux.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace MobileAppForms.Droid
{
    public class BorderlessEntryRenderer : EntryRenderer
	{
		#region properties

		BorderlessEntry CustomElement;

		#endregion


		#region ctor(s)

		public BorderlessEntryRenderer(Context context) : base(context)
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
			var ignore1 = typeof(BorderlessEntryRenderer);
			var ignore2 = typeof(BorderlessEntry);
#pragma warning restore 0219
		}

		#endregion

		#region overrides

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				CustomElement = e.NewElement as BorderlessEntry;
				CustomElement.OnFocusChanged += CustomElement_OnFocusChanged;
				Control.Background = null;
				Control.ImeOptions = (ImeAction)ImeFlags.NoExtractUi;
				
				var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
				layoutParams.SetMargins(0, 0, 0, 0);
				LayoutParameters = layoutParams;
				Control.LayoutParameters = layoutParams;
				Control.SetPadding(0, 0, 0, 0);
				SetPadding(0, 0, 0, 0);
			}

			/*
			if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
			{
				// Control.SetTextCursorDrawable(Resource.Drawable.custom_cursor); //This API Intrduced in android 10
			}
			else
			{
				// IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
				// IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
				// JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.custom_cursor);
			}
			*/
		}

		#endregion

		#region event handler

		private void CustomElement_OnFocusChanged(object sender, FocusEventArgs e)
		{
			try
			{
				Control?.SetSelection(Control.Text.Length);
			}
			catch (Exception) { }
			// editText.SetSelection(editText.Text.Length);
		}

		#endregion
	}
}