using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Common
{
    public partial class OverlayBackdrop : BoxView
    {
        #region bindable properties

        public static BindableProperty AnimationDurationProperty = BindableProperty.Create(nameof(AnimationDuration), typeof(uint), typeof(OverlayBackdrop), defaultValue: 150u);

        public static BindableProperty ShownOpacityProperty = BindableProperty.Create(nameof(ShownOpacity), typeof(double), typeof(OverlayBackdrop), defaultValue: 0.4d);

        #endregion

        #region properties

        public uint AnimationDuration
        {
            get => (uint)GetValue(AnimationDurationProperty);
            set => SetValue(AnimationDurationProperty, value);
        }

        public double ShownOpacity
        {
            get => (double)GetValue(ShownOpacityProperty);
            set => SetValue(ShownOpacityProperty, value);
        }

        #endregion

        #region ctor(s)

        public OverlayBackdrop()
        {
            InitializeComponent();
        }

        #endregion

        #region access methods

        public async Task Show()
        {
            this.IsVisible = true;
            this.InputTransparent = false;
            await this.FadeTo(ShownOpacity, AnimationDuration);
        }

        public async Task Hide()
        {
            await this.FadeTo(0, AnimationDuration);
            this.IsVisible = false;
            this.InputTransparent = true;
        }

        #endregion
    }
}
