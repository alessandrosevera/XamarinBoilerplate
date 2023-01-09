using System;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Controls
{
    public partial class BorderlessEntry : Entry
    {
        #region bindable properties

        public static BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(nameof(PlaceholderFontFamily), typeof(string), typeof(BorderlessEntry), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) =>
        {
        });

        #endregion

        #region properties

        public string PlaceholderFontFamily
        {
            get => (string)GetValue(PlaceholderFontFamilyProperty);
            set => SetValue(PlaceholderFontFamilyProperty, value);
        }

        #endregion

        #region event properties

        public event EventHandler<FocusEventArgs> OnFocusChanged;

        #endregion

        #region ctor

        public BorderlessEntry()
        {
            //InitializeComponent();
        }

        #endregion

        #region event handler

        public void RaiseFocusChanged(bool isFocused)
        {
            OnFocusChanged?.Invoke(this, new FocusEventArgs(this, isFocused));
        }

        #endregion
    }
}
