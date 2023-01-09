using System;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Controls
{
    public class MaterialListView : ListView
    {
        #region bindable properties

        public static BindableProperty ClipToPaddingProperty
            = BindableProperty.Create(nameof(ClipToPadding), typeof(bool), typeof(MaterialScrollView), false, propertyChanged: null);

        public static BindableProperty ContentInsetProperty
            = BindableProperty.Create(nameof(ContentInset), typeof(Thickness), typeof(MaterialScrollView), new Thickness(0), propertyChanged: null);

        #endregion

        #region properties

        public bool ClipToPadding
        {
            get => (bool)GetValue(ClipToPaddingProperty);
            set => SetValue(ClipToPaddingProperty, value);
        }

        public Thickness ContentInset
        {
            get => (Thickness)GetValue(ContentInsetProperty);
            set => SetValue(ContentInsetProperty, value);
        }

        #endregion
    }
}
