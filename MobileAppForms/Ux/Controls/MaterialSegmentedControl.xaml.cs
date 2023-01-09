using System;
using System.Collections.Generic;
using PropertyChanged;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace MobileAppForms.Ux.Controls
{
    public enum Segment
    {
        Left,
        Right
    }

    public class SegmentChangedEventArgs : EventArgs
    {
        public Segment? NewValue { get; }
        public Segment? OldValue { get; }

        public SegmentChangedEventArgs(Segment? oldValue, Segment? newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public partial class MaterialSegmentedControl : PancakeView
    {
        #region bindable properties

        public static BindableProperty LeftSegmentTextProperty
            = BindableProperty.Create(nameof(LeftSegmentText), typeof(string), typeof(MaterialSegmentedControl), nameof(Segment.Left),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is MaterialSegmentedControl materialSegmentedControl)
                    {
                        materialSegmentedControl.OnLayoutPropertyChanged();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[MaterialSegmentedControl] bindable element is wrong type!");
                    }
                });

        public static BindableProperty RightSegmentTextProperty
            = BindableProperty.Create(nameof(RightSegmentText), typeof(string), typeof(MaterialSegmentedControl), nameof(Segment.Right),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is MaterialSegmentedControl materialSegmentedControl)
                    {
                        materialSegmentedControl.OnLayoutPropertyChanged();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[MaterialSegmentedControl] bindable element is wrong type!");
                    }
                });

        public static BindableProperty SegmentsFontFamilyProperty
            = BindableProperty.Create(nameof(SegmentsFontFamily), typeof(string), typeof(MaterialSegmentedControl), null,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is MaterialSegmentedControl materialSegmentedControl)
                    {
                        materialSegmentedControl.OnLayoutPropertyChanged();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[MaterialSegmentedControl] bindable element is wrong type!");
                    }
                });

        public static BindableProperty SegmentsFontSizeProperty
            = BindableProperty.Create(nameof(SegmentsFontSize), typeof(double), typeof(MaterialSegmentedControl), 14d,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is MaterialSegmentedControl materialSegmentedControl)
                    {
                        materialSegmentedControl.OnLayoutPropertyChanged();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[MaterialSegmentedControl] bindable element is wrong type!");
                    }
                });

        public static BindableProperty SegmentsTextColorProperty
            = BindableProperty.Create(nameof(SegmentsTextColor), typeof(Color), typeof(MaterialSegmentedControl), Color.FromHex("#484451"),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is MaterialSegmentedControl materialSegmentedControl)
                    {
                        materialSegmentedControl.OnLayoutPropertyChanged();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[MaterialSegmentedControl] bindable element is wrong type!");
                    }
                });

        public static BindableProperty SegmentsPaddingProperty
            = BindableProperty.Create(nameof(SegmentsPadding), typeof(Thickness), typeof(MaterialSegmentedControl), new Thickness(0, 14),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is MaterialSegmentedControl materialSegmentedControl)
                    {
                        materialSegmentedControl.OnLayoutPropertyChanged();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[MaterialSegmentedControl] bindable element is wrong type!");
                    }
                });

        public static BindableProperty SegmentsRadiusProperty
            = BindableProperty.Create(nameof(SegmentsRadius), typeof(CornerRadius), typeof(MaterialSegmentedControl), new CornerRadius(10),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is MaterialSegmentedControl materialSegmentedControl)
                    {
                        materialSegmentedControl.OnLayoutPropertyChanged();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[MaterialSegmentedControl] bindable element is wrong type!");
                    }
                });

        public static BindableProperty SelectedSegmentsTextColorProperty
            = BindableProperty.Create(nameof(SelectedSegmentsTextColor), typeof(Color), typeof(MaterialSegmentedControl), Color.White,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is MaterialSegmentedControl materialSegmentedControl)
                    {
                        materialSegmentedControl.OnLayoutPropertyChanged();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[MaterialSegmentedControl] bindable element is wrong type!");
                    }
                });

        public static BindableProperty SelectedSegmentBackgroundColorProperty
            = BindableProperty.Create(nameof(SelectedSegmentBackgroundColor), typeof(Color), typeof(MaterialSegmentedControl), Color.FromHex("#589FF8"),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is MaterialSegmentedControl materialSegmentedControl)
                    {
                        materialSegmentedControl.OnLayoutPropertyChanged();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[MaterialSegmentedControl] bindable element is wrong type!");
                    }
                });

        public static BindableProperty SelectedValueProperty
            = BindableProperty.Create(nameof(SelectedValue), typeof(Segment?), typeof(MaterialSegmentedControl), Segment.Left,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    if (bindable is MaterialSegmentedControl materialSegmentedControl)
                    {
                        materialSegmentedControl.OnLayoutPropertyChanged();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[MaterialSegmentedControl] bindable element is wrong type!");
                    }
                });

        public static BindableProperty EnableEmptyValueProperty
            = BindableProperty.Create(nameof(EnableEmptyValue), typeof(bool), typeof(MaterialSegmentedControl), false, propertyChanged: null);

        #endregion

        #region properties

        public string LeftSegmentText
        {
            get => (string)GetValue(LeftSegmentTextProperty);
            set => SetValue(LeftSegmentTextProperty, value);
        }

        public string RightSegmentText
        {
            get => (string)GetValue(RightSegmentTextProperty);
            set => SetValue(RightSegmentTextProperty, value);
        }

        public string SegmentsFontFamily
        {
            get => (string)GetValue(SegmentsFontFamilyProperty);
            set => SetValue(SegmentsFontFamilyProperty, value);
        }

        public double SegmentsFontSize
        {
            get => (double)GetValue(SegmentsFontSizeProperty);
            set => SetValue(SegmentsFontSizeProperty, value);
        }

        public Color SegmentsTextColor
        {
            get => (Color)GetValue(SegmentsTextColorProperty);
            set => SetValue(SegmentsTextColorProperty, value);
        }

        public Thickness SegmentsPadding
        {
            get => (Thickness)GetValue(SegmentsPaddingProperty);
            set => SetValue(SegmentsPaddingProperty, value);
        }

        public CornerRadius SegmentsRadius
        {
            get => (CornerRadius)GetValue(SegmentsRadiusProperty);
            set => SetValue(SegmentsRadiusProperty, value);
        }

        public Color SelectedSegmentsTextColor
        {
            get => (Color)GetValue(SelectedSegmentsTextColorProperty);
            set => SetValue(SelectedSegmentsTextColorProperty, value);
        }

        public Color SelectedSegmentBackgroundColor
        {
            get => (Color)GetValue(SelectedSegmentBackgroundColorProperty);
            set => SetValue(SelectedSegmentBackgroundColorProperty, value);
        }

        public Segment? SelectedValue
        {
            get => (Segment?)GetValue(SelectedValueProperty);
            set => SetValue(SelectedValueProperty, value);
        }

        public bool EnableEmptyValue
        {
            get => (bool)GetValue(EnableEmptyValueProperty);
            set => SetValue(EnableEmptyValueProperty, value);
        }

        #endregion

        #region auto-properties

        private Action<Segment?> ActOnValueChanged { get; set; }

        #endregion

        #region events

        public event EventHandler<SegmentChangedEventArgs> Changed;

        #endregion

        #region ctor(s)

        public MaterialSegmentedControl()
        {
            InitializeComponent();
        }

        #endregion

        #region access methods

        public void Configure(Action<Segment?> actOnValueChanged)
        {
            ActOnValueChanged = actOnValueChanged;
        }

        #endregion

        #region event handlers

        [SuppressPropertyChangedWarnings]
        internal void OnLayoutPropertyChanged()
        {
            this.CornerRadius = SegmentsRadius;

            LeftSegmentFrame.Padding = SegmentsPadding;
            RightSegmentFrame.Padding = SegmentsPadding;

            LeftSegmentLabel.TextColor = SegmentsTextColor;
            RightSegmentLabel.TextColor = SegmentsTextColor;

            SegmentSeparator.Color = SegmentsTextColor;
            if (!(this.Border is null)) this.Border.Color = SegmentsTextColor;

            LeftSegmentLabel.FontFamily = SegmentsFontFamily;
            RightSegmentLabel.FontFamily = SegmentsFontFamily;

            LeftSegmentLabel.FontSize = SegmentsFontSize;
            RightSegmentLabel.FontSize = SegmentsFontSize;

            LeftSegmentLabel.Text = LeftSegmentText;
            RightSegmentLabel.Text = RightSegmentText;

            if (SelectedValue.HasValue)
            {
                switch (SelectedValue.Value)
                {
                    case Segment.Left:
                        RightSegmentFrame.BackgroundColor = Color.Transparent;
                        RightSegmentLabel.TextColor = SegmentsTextColor;

                        LeftSegmentFrame.BackgroundColor = SelectedSegmentBackgroundColor;
                        LeftSegmentLabel.TextColor = SelectedSegmentsTextColor;
                        break;
                    case Segment.Right:
                        LeftSegmentFrame.BackgroundColor = Color.Transparent;
                        LeftSegmentLabel.TextColor = SegmentsTextColor;

                        RightSegmentFrame.BackgroundColor = SelectedSegmentBackgroundColor;
                        RightSegmentLabel.TextColor = SelectedSegmentsTextColor;
                        break;
                }
            }
            else
            {
                LeftSegmentFrame.BackgroundColor = Color.Transparent;
                LeftSegmentLabel.TextColor = SegmentsTextColor;

                RightSegmentFrame.BackgroundColor = Color.Transparent;
                RightSegmentLabel.TextColor = SegmentsTextColor;
            }

            // SetValueLabelBinding(LeftSegmentLabel, LeftSegmentTextProperty);
            // SetValueLabelBinding(RightSegmentLabel, RightSegmentTextProperty);
        }

        private void HandleLeftSegmentTapped(System.Object sender, System.EventArgs e)
        {
            if (SelectedValue != Segment.Left)
            {
                SelectedValue = Segment.Left;
                Changed?.Invoke(this, new SegmentChangedEventArgs(Segment.Right, SelectedValue));
                if (!(ActOnValueChanged is null)) ActOnValueChanged(Segment.Left);
            }
            else if (EnableEmptyValue)
            {
                SelectedValue = null;
                Changed?.Invoke(this, new SegmentChangedEventArgs(Segment.Left, SelectedValue));
                if (!(ActOnValueChanged is null)) ActOnValueChanged(null);
            }
        }

        private void HandleRightSegmentTapped(System.Object sender, System.EventArgs e)
        {
            if (SelectedValue != Segment.Right)
            {
                SelectedValue = Segment.Right;
                Changed?.Invoke(this, new SegmentChangedEventArgs(Segment.Left, SelectedValue));
                if (!(ActOnValueChanged is null)) ActOnValueChanged(Segment.Right);
            }
            else if (EnableEmptyValue)
            {
                SelectedValue = null;
                Changed?.Invoke(this, new SegmentChangedEventArgs(Segment.Right, SelectedValue));
                if (!(ActOnValueChanged is null)) ActOnValueChanged(null);
            }
        }

        #endregion

        #region helper methods

        private void SetValueLabelBinding(Label label, BindableProperty bindableProperty)
            => label.SetBinding(Label.TextProperty, new Binding
            {
                Path = bindableProperty.PropertyName,
                Source = this
            });

        #endregion

    }
}
