using System.ComponentModel;
using Foundation;
using UIKit;
using MobileAppForms.iOS;
using MobileAppForms.Ux.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace MobileAppForms.iOS
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        #region auto-properties

        private BorderlessEntry FormsElement { get; set; }

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

            if (!(e.NewElement is null) && e.NewElement is BorderlessEntry borderlessEntry)
            {
                FormsElement = borderlessEntry;

                Control.AutocorrectionType = UITextAutocorrectionType.No;
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;

                UpdatePlaceholderFont();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (!(Control is null))
            {
                // Control.Layer.BorderWidth = 0;
                // Control.BorderStyle = UITextBorderStyle.None;

                if (e.PropertyName == nameof(FormsElement.PlaceholderFontFamily))
                    UpdatePlaceholderFont();
            }
        }

        #endregion


        #region helper methods

        private void UpdatePlaceholderFont()
        {
            if (!(FormsElement is null) && FormsElement.PlaceholderFontFamily != FormsElement.FontFamily)
            {
                string placeholderFontFamily = !string.IsNullOrEmpty(FormsElement.PlaceholderFontFamily) ? FormsElement.PlaceholderFontFamily : FormsElement.FontFamily;
                var descriptor = new UIFontDescriptor().CreateWithFamily(placeholderFontFamily);
                var placeholderFont = UIFont.FromDescriptor(descriptor, (float)FormsElement.FontSize);
                Control.AttributedPlaceholder = new NSAttributedString(FormsElement.Placeholder, placeholderFont, FormsElement.PlaceholderColor.ToUIColor());
            }
        }

        #endregion
    }
}
