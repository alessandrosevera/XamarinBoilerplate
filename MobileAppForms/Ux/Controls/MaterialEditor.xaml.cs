using System;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace MobileAppForms.Ux.Controls
{
    public partial class MaterialEditor : PancakeView
    {
        #region bindable properties

        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialEditor), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            //matEditor.EditorField.Text = (string)newVal;
        });

        public static BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialEditor), defaultValue: Keyboard.Default, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            matEditor.EditorField.Keyboard = (Keyboard)newVal;
        });

        public static BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialEditor), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            matEditor.EditorField.FontFamily = (string)newVal;
            if (string.IsNullOrEmpty(matEditor.PlaceholderFontFamily))
            {
                matEditor.EditorPlaceholder.FontFamily = (string)newVal;
            }
        });

        public static BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(MaterialEditor), defaultValue: Color.FromHex("#80696C6F"), propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            matEditor.EditorField.PlaceholderColor = (Color)newVal;
            matEditor.EditorPlaceholder.TextColor = (Color)newVal;
        });

        public static BindableProperty PlaceholderFontFamilyProperty = BindableProperty.Create(nameof(PlaceholderFontFamily), typeof(string), typeof(MaterialEditor), defaultValue: null, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            // matEditor.EditorField.PlaceholderFontFamily = (string)newVal;
            matEditor.EditorPlaceholder.FontFamily = (string)newVal;
        });

        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialEditor), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEditor = (MaterialEditor)bindable;
            if (!matEditor.MustMantainPlaceholderOnValueSet) matEditor.EditorField.Placeholder = (string)newval;
            matEditor.EditorPlaceholder.Text = (string)newval;
        });

        public static BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialEditor), defaultValue: Color.FromHex("#FF1D204C"), propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            matEditor.EditorField.TextColor = (Color)newVal;
        });

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialEditor), defaultValue: 12.0d, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            matEditor.EditorField.FontSize = (double)newVal;
            matEditor.EditorPlaceholder.FontSize = (double)newVal;
        });

        public static BindableProperty UseWrappingBorderProperty = BindableProperty.Create(nameof(UseWrappingBorder), typeof(bool), typeof(MaterialEditor), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            bool useBorder = (bool)newVal;
            if (useBorder)
            {
                matEditor.Line.IsVisible = false;
                matEditor.AccentLine.IsVisible = false;
                matEditor.ErrorLine.IsVisible = false;
                Grid.SetRowSpan(matEditor.EditorField, 2);
                Grid.SetRowSpan(matEditor.EditorPlaceholder, 2);

                var border = new Border();
                border.Thickness = matEditor.BorderThickness;
                border.Color = matEditor.BorderColor;
                border.DrawingStyle = matEditor.BorderDrawingStyle;
                matEditor.Border = border;
                matEditor.CornerRadius = matEditor.CornerRadius;
            }
            else
            {
                Grid.SetRowSpan(matEditor.EditorField, 0);
                Grid.SetRowSpan(matEditor.EditorPlaceholder, 0);
                matEditor.CornerRadius = 0;
                matEditor.Border = null;

                matEditor.AdaptElements(matEditor.EditorField.IsFocused);
            }
        });

        public static BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(MaterialEditor), defaultValue: 0, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            if (matEditor.UseWrappingBorder)
            {
                if (matEditor.Border is null)
                {
                    var border = new Border();
                    border.Thickness = (int)newVal;
                    border.Color = matEditor.BorderColor;
                    border.DrawingStyle = matEditor.BorderDrawingStyle;
                    matEditor.Border = border;
                }
                else
                {
                    matEditor.Border.Thickness = (int)newVal;
                }
            }
        });

        public static BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialEditor), defaultValue: Color.Transparent, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            if (matEditor.UseWrappingBorder)
            {
                if (matEditor.Border is null)
                {
                    var border = new Border();
                    border.Thickness = matEditor.BorderThickness;
                    border.Color = (Color)newVal;
                    border.DrawingStyle = matEditor.BorderDrawingStyle;
                    matEditor.Border = border;
                }
                else
                {
                    matEditor.Border.Color = (Color)newVal;
                }
            }
        });

        public static BindableProperty BorderDrawingStyleProperty = BindableProperty.Create(nameof(BorderDrawingStyle), typeof(BorderDrawingStyle), typeof(MaterialEditor), defaultValue: BorderDrawingStyle.Outside, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            if (matEditor.UseWrappingBorder)
            {
                if (matEditor.Border is null)
                {
                    var border = new Border();
                    border.Thickness = matEditor.BorderThickness;
                    border.Color = matEditor.BorderColor;
                    border.DrawingStyle = (BorderDrawingStyle)newVal;
                    matEditor.Border = border;
                }
                else
                {
                    matEditor.Border.DrawingStyle = (BorderDrawingStyle)newVal;
                }
            }
        });

        public static BindableProperty MustMantainAccentOnValueSetProperty = BindableProperty.Create(nameof(MustMantainAccentOnValueSet), typeof(bool), typeof(MaterialEditor), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
        {

        });

        public static BindableProperty InputTranslationYOnValueSetProperty = BindableProperty.Create(nameof(InputTranslationYOnValueSet), typeof(double), typeof(MaterialEntry), defaultValue: 9d, propertyChanged: (bindable, oldVal, newVal) =>
        {
        });

        public static BindableProperty MustMantainPlaceholderOnValueSetProperty = BindableProperty.Create(nameof(MustMantainPlaceholderOnValueSet), typeof(bool), typeof(MaterialEditor), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            bool boolVal = (bool)newVal;

            if (boolVal) matEditor.EditorField.Placeholder = string.Empty;
            else matEditor.EditorField.Placeholder = matEditor.Placeholder;
            matEditor.EditorPlaceholder.IsVisible = boolVal;
        });

        public static BindableProperty PlaceholderReductionOnValueSetProperty = BindableProperty.Create(nameof(PlaceholderReductionOnValueSet), typeof(int), typeof(MaterialEditor), defaultValue: 2, propertyChanged: (bindable, oldVal, newVal) =>
        {
        });

        public static BindableProperty LineColorProperty = BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(MaterialEditor), defaultValue: Color.FromHex("#FF8C8C8C"), propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            matEditor.Line.BackgroundColor = matEditor.Line.Color = (Color)newVal;
        });

        public static BindableProperty AccentColorProperty = BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(MaterialEditor), defaultValue: Color.DodgerBlue, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            matEditor.AccentLine.BackgroundColor = matEditor.AccentLine.Color = (Color)newVal;
        });

        public static BindableProperty ErrorColorProperty = BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(MaterialEditor), defaultValue: Color.Red, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEditor = (MaterialEditor)bindable;
            matEditor.ErrorLine.BackgroundColor = matEditor.ErrorLine.Color = (Color)newVal;
        });

        public static BindableProperty ReferenceEntryHeightProperty = BindableProperty.Create(nameof(ReferenceEntryHeight), typeof(double), typeof(MaterialEditor), defaultValue: 17d, propertyChanged: (bindable, oldVal, newVal) =>
        { });

        #endregion


        #region auto-properties

        public bool HasError { get; private set; }

        #endregion


        #region properties

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }

        public string PlaceholderFontFamily
        {
            get => (string)GetValue(PlaceholderFontFamilyProperty);
            set => SetValue(PlaceholderFontFamilyProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public bool UseWrappingBorder
        {
            get => (bool)GetValue(UseWrappingBorderProperty);
            set => SetValue(UseWrappingBorderProperty, value);
        }

        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public BorderDrawingStyle BorderDrawingStyle
        {
            get => (BorderDrawingStyle)GetValue(BorderDrawingStyleProperty);
            set => SetValue(BorderDrawingStyleProperty, value);
        }

        public bool MustMantainAccentOnValueSet
        {
            get => (bool)GetValue(MustMantainAccentOnValueSetProperty);
            set => SetValue(MustMantainAccentOnValueSetProperty, value);
        }

        public double InputTranslationYOnValueSet
        {
            get => (double)GetValue(InputTranslationYOnValueSetProperty);
            set => SetValue(InputTranslationYOnValueSetProperty, value);
        }

        public bool MustMantainPlaceholderOnValueSet
        {
            get => (bool)GetValue(MustMantainPlaceholderOnValueSetProperty);
            set => SetValue(MustMantainPlaceholderOnValueSetProperty, value);
        }

        public int PlaceholderReductionOnValueSet
        {
            get => (int)GetValue(PlaceholderReductionOnValueSetProperty);
            set => SetValue(PlaceholderReductionOnValueSetProperty, value);
        }

        public Color LineColor
        {
            get => (Color)GetValue(LineColorProperty);
            set => SetValue(LineColorProperty, value);
        }

        public Color AccentColor
        {
            get => (Color)GetValue(AccentColorProperty);
            set => SetValue(AccentColorProperty, value);
        }

        public Color ErrorColor
        {
            get => (Color)GetValue(ErrorColorProperty);
            set => SetValue(ErrorColorProperty, value);
        }

        public double ReferenceEntryHeight
        {
            get => (double)GetValue(ReferenceEntryHeightProperty);
            set => SetValue(ReferenceEntryHeightProperty, value);
        }

        #endregion

        #region events

        public event EventHandler<TextChangedEventArgs> TextChanged;
        public event EventHandler<FocusEventArgs> EditorFocused;

        #endregion

        #region ctor(s)

        public MaterialEditor()
        {
            InitializeComponent();

            AdaptElements(false);
            EditorField.BindingContext = this;
        }

        #endregion

        #region access methods

        public void SetError(bool hasError)
        {
            HasError = hasError;
            AdaptElements(EditorField.IsFocused);
        }

        #endregion

        #region helper methods

        private void AdaptElements(bool isEntryFocused)
        {
            if (MustMantainPlaceholderOnValueSet)
            {
                if (!string.IsNullOrEmpty(EditorField.Text))
                {
                    var editorFieldTranslationY = UseWrappingBorder ? InputTranslationYOnValueSet : 0;
                    var scale = 1 - (PlaceholderReductionOnValueSet / FontSize);

                    if (UseWrappingBorder) EditorField.TranslateTo(0, editorFieldTranslationY);
                    EditorPlaceholder.ScaleTo(scale);
                    EditorPlaceholder.TranslateTo(0, -(ReferenceEntryHeight - editorFieldTranslationY));
                }
                else
                {
                    if (UseWrappingBorder) EditorField.TranslateTo(0, 0);
                    EditorPlaceholder.ScaleTo(1);
                    EditorPlaceholder.TranslateTo(0, 0);
                }
            }

            if (!isEntryFocused)
            {
                if (HasError)
                {
                    if (!UseWrappingBorder)
                    {
                        ErrorLine.IsVisible = true;
                        Line.IsVisible = false;
                    }
                    else
                    {
                        this.Border.Color = ErrorColor;
                    }
                }
                else
                {
                    if (!UseWrappingBorder)
                    {
                        ErrorLine.IsVisible = false;
                        Line.IsVisible = true;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(EditorField.Text) && MustMantainAccentOnValueSet)
                        {
                            this.Border.Color = AccentColor;
                        }
                        else
                        {
                            this.Border.Color = BorderColor;
                        }
                    }
                }

                AccentLine.IsVisible = false;
            }
            else
            {
                if (!UseWrappingBorder)
                {
                    Line.IsVisible = false;
                    AccentLine.IsVisible = true;
                    ErrorLine.IsVisible = false;
                }
                else
                {
                    this.Border.Color = AccentColor;
                }
            }
        }

        private void RaiseFocused(bool isFocused)
        {
            EventHandler<FocusEventArgs> handler = EditorFocused;
            if (handler != null)
            {
                var e = new FocusEventArgs(this, isFocused);
                handler(this, e);
            }
        }

        #endregion

        #region event handlers

        private void HandleEditorFieldTextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            AdaptElements(EditorField.IsFocused);

            EventHandler<TextChangedEventArgs> handler = TextChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void HandleEditorFieldFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            // await Task.Delay(250);

            AdaptElements(e.IsFocused);
            RaiseFocused(e.IsFocused);

            // if (DeviceInfo.Platform == DevicePlatform.Android) EditorField.RaiseFocusChanged(true);
        }

        #endregion
    }
}
