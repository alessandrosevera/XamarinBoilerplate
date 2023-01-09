using System;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Controls
{
    public class MaterialScrollView : ScrollView
    {
        #region bindable properties

        public static BindableProperty IsScrollEnabledProperty
            = BindableProperty.Create(nameof(IsScrollEnabled), typeof(bool), typeof(MaterialScrollView), true, propertyChanged: null);

        #endregion

        #region properties

        public bool IsScrollEnabled
        {
            get => (bool)GetValue(IsScrollEnabledProperty);
            set => SetValue(IsScrollEnabledProperty, value);
        }

        #endregion

        #region ctor(s)

        public MaterialScrollView()
        {
        }

        #endregion
    }
}
