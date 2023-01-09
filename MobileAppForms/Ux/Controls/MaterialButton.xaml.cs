using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace MobileAppForms.Ux.Controls
{
    public partial class MaterialButton : ContentView
    {

        #region bindable properties

        public static BindableProperty ButtonHeightProperty = BindableProperty.Create(nameof(ButtonHeight), typeof(double), typeof(MaterialButton), defaultValue: -1d,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button) button.ThisButton.HeightRequest = (double)newVal;
            });

        public static BindableProperty ButtonBackgroundColorProperty = BindableProperty.Create(nameof(ButtonBackgroundColor), typeof(Color), typeof(MaterialButton), defaultValue: Color.FromHex("#589FF8"),
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button) button.ThisButton.BackgroundColor = (Color)newVal;
            });

        public static BindableProperty ButtonFontFamilyProperty = BindableProperty.Create(nameof(ButtonFontFamily), typeof(string), typeof(MaterialButton), defaultValue: null,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button) button.MyText.FontFamily = (string)newVal;
            });

        public static BindableProperty ButtonFontSizeProperty = BindableProperty.Create(nameof(ButtonFontSize), typeof(double), typeof(MaterialButton), defaultValue: 14d,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button) button.MyText.FontSize = (double)newVal;
            });

        public static BindableProperty ButtonTextProperty = BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(MaterialButton), defaultValue: string.Empty,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button && newVal is string newButtonText) button.MyText.Text = newButtonText;
            });

        public static BindableProperty ButtonTextTransformProperty = BindableProperty.Create(nameof(ButtonTextTransform), typeof(TextTransform), typeof(MaterialButton), defaultValue: TextTransform.Default,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button && newVal is TextTransform newButtonTextTransform) button.MyText.TextTransform = newButtonTextTransform;
            });

        public static BindableProperty ButtonIconProperty = BindableProperty.Create(nameof(ButtonIcon), typeof(string), typeof(MaterialButton), defaultValue: null,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button && newVal is string newIconSource) button.MyIcon.Source = ImageSource.FromFile(newIconSource);
            });

        public static BindableProperty ButtonIconSizeProperty = BindableProperty.Create(nameof(ButtonIconSize), typeof(double), typeof(MaterialButton), defaultValue: 18d,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button)
                {
                    button.MyIcon.HeightRequest = (double)newVal;
                }
            });

        public static BindableProperty UseIconProperty = BindableProperty.Create(nameof(UseIcon), typeof(bool), typeof(MaterialButton), defaultValue: false,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button)
                {
                    bool useIcon = (bool)newVal;
                    // button.MyText.WidthRequest = useIcon ? 0 : -1;
                    if (useIcon)
                    {
                        button.MyIcon.HeightRequest = button.ButtonIconSize;
                        button.MyIcon.WidthRequest = -1;
                    }
                    else
                    {
                        button.MyIcon.WidthRequest = button.MyIcon.HeightRequest = 0;
                    }

                    button.MyIcon.IsVisible = useIcon;
                    // button.MyText.IsVisible = !button.MyIcon.IsVisible;
                }
            });

        public static BindableProperty ButtonTextColorProperty = BindableProperty.Create(nameof(ButtonTextColor), typeof(Color), typeof(MaterialButton), defaultValue: Color.White,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button) button.MyText.TextColor = (Color)newVal;
            });

        public static BindableProperty ButtonPaddingProperty = BindableProperty.Create(nameof(ButtonPadding), typeof(Thickness), typeof(MaterialButton), defaultValue: new Thickness(34, 19),
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button) button.ThisButton.Padding = (Thickness)newVal;
            });

        public static BindableProperty ButtonRadiusProperty = BindableProperty.Create(nameof(ButtonRadius), typeof(CornerRadius), typeof(MaterialButton), defaultValue: new CornerRadius(28),
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button) button.ThisButton.CornerRadius = (CornerRadius)newVal;
            });

        public static BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(MaterialButton), defaultValue: true,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button)
                {
                    if ((bool)newVal)
                    {
                        var dropShadow = new Xamarin.Forms.PancakeView.DropShadow()
                        {
                            BlurRadius = button.ShadowBlur,
                            Color = button.ShadowColor,
                            Offset = button.ShadowOffset
                        };
                        button.ThisButton.Shadow = dropShadow;
                    }
                    else
                    {
                        button.ThisButton.Shadow = null;
                    }
                }
            });

        public static BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(MaterialButton), defaultValue: Color.FromHex("#1A000000"),
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button) button.ThisButton.Shadow.Color = (Color)newVal;
            });

        public static BindableProperty ShadowOffsetProperty = BindableProperty.Create(nameof(ShadowOffset), typeof(Point), typeof(MaterialButton), defaultValue: new Point(0, 8),
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button) button.ThisButton.Shadow.Offset = (Point)newVal;
            });

        public static BindableProperty ShadowBlurProperty = BindableProperty.Create(nameof(ShadowBlur), typeof(float), typeof(MaterialButton), defaultValue: 16.0f,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button) button.ThisButton.Shadow.BlurRadius = (float)newVal;
            });

        public static BindableProperty LoadingColorProperty = BindableProperty.Create(nameof(LoadingColor), typeof(Color), typeof(MaterialButton), defaultValue: Color.White,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button) button.MyLoading.Color = (Color)newVal;
            });

        public static BindableProperty MustReduceToLoadingProperty = BindableProperty.Create(nameof(MustReduceToLoading), typeof(bool), typeof(MaterialButton), defaultValue: true,
            propertyChanged: (bindable, old, newVal) =>
            {
            });

        public static BindableProperty IsLoadingProperty = BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(MaterialButton), defaultValue: false,
            propertyChanged: (bindable, old, newVal) =>
            {
                if (bindable is MaterialButton button)
                {
                    bool boolVal = (bool)newVal;
                    if (boolVal != button.IsActing)
                    {
                        button.IsActing = boolVal;
                        button.UpdateActingUI();
                    }
                }
            });

        public static BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(MaterialButton), defaultValue: 0, propertyChanged: (bindable, oldVal, newVal) =>
        {
            if (bindable is MaterialButton button)
            {
                if (button.ThisButton.Border is null)
                {
                    var border = new Border();
                    border.Thickness = (int)newVal;
                    border.Color = button.BorderColor;
                    border.DrawingStyle = button.BorderDrawingStyle;
                    button.ThisButton.Border = border;
                }
                else
                {
                    button.ThisButton.Border.Thickness = (int)newVal;
                }
            }
        });

        public static BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialButton), defaultValue: Color.Transparent, propertyChanged: (bindable, oldVal, newVal) =>
        {
            if (bindable is MaterialButton button)
            {
                if (button.ThisButton.Border is null)
                {
                    var border = new Border();
                    border.Thickness = button.BorderThickness;
                    border.Color = (Color)newVal;
                    border.DrawingStyle = button.BorderDrawingStyle;
                    button.ThisButton.Border = border;
                }
                else
                {
                    button.ThisButton.Border.Color = (Color)newVal;
                }
            }
        });

        public static BindableProperty BorderDrawingStyleProperty = BindableProperty.Create(nameof(BorderDrawingStyle), typeof(BorderDrawingStyle), typeof(MaterialButton), defaultValue: BorderDrawingStyle.Outside, propertyChanged: (bindable, oldVal, newVal) =>
        {
            if (bindable is MaterialButton button)
            {
                if (button.ThisButton.Border is null)
                {
                    var border = new Border();
                    border.Thickness = button.BorderThickness;
                    border.Color = button.BorderColor;
                    border.DrawingStyle = (BorderDrawingStyle)newVal;
                    button.ThisButton.Border = border;
                }
                else
                {
                    button.ThisButton.Border.DrawingStyle = (BorderDrawingStyle)newVal;
                }
            }
        });

        #endregion

        #region properties

        public double ButtonHeight
        {
            get => (double)GetValue(ButtonHeightProperty);
            set => SetValue(ButtonHeightProperty, value);
        }
        

        public Color ButtonBackgroundColor
        {
            get => (Color)GetValue(ButtonBackgroundColorProperty);
            set => SetValue(ButtonBackgroundColorProperty, value);
        }

        public string ButtonFontFamily
        {
            get => (string)GetValue(ButtonFontFamilyProperty);
            set => SetValue(ButtonFontFamilyProperty, value);
        }

        public double ButtonFontSize
        {
            get => (double)GetValue(ButtonFontSizeProperty);
            set => SetValue(ButtonFontSizeProperty, value);
        }

        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }

        public TextTransform ButtonTextTransform
        {
            get => (TextTransform)GetValue(ButtonTextTransformProperty);
            set => SetValue(ButtonTextTransformProperty, value);
        }

        public string ButtonIcon
        {
            get => (string)GetValue(ButtonIconProperty);
            set => SetValue(ButtonIconProperty, value);
        }

        public double ButtonIconSize
        {
            get => (double)GetValue(ButtonIconSizeProperty);
            set => SetValue(ButtonIconSizeProperty, value);
        }

        public bool UseIcon
        {
            get => (bool)GetValue(UseIconProperty);
            set => SetValue(UseIconProperty, value);
        }

        public Color ButtonTextColor
        {
            get => (Color)GetValue(ButtonTextColorProperty);
            set => SetValue(ButtonTextColorProperty, value);
        }

        public Thickness ButtonPadding
        {
            get => (Thickness)GetValue(ButtonPaddingProperty);
            set => SetValue(ButtonPaddingProperty, value);
        }

        public CornerRadius ButtonRadius
        {
            get => (CornerRadius)GetValue(ButtonRadiusProperty);
            set => SetValue(ButtonRadiusProperty, value);
        }

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public Color ShadowColor
        {
            get => (Color)GetValue(ShadowColorProperty);
            set => SetValue(ShadowColorProperty, value);
        }

        public Point ShadowOffset
        {
            get => (Point)GetValue(ShadowOffsetProperty);
            set => SetValue(ShadowOffsetProperty, value);
        }

        public float ShadowBlur
        {
            get => (float)GetValue(ShadowBlurProperty);
            set => SetValue(ShadowBlurProperty, value);
        }

        public Color LoadingColor
        {
            get => (Color)GetValue(LoadingColorProperty);
            set => SetValue(LoadingColorProperty, value);
        }

        public bool MustReduceToLoading
        {
            get => (bool)GetValue(MustReduceToLoadingProperty);
            set => SetValue(MustReduceToLoadingProperty, value);
        }

        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
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

        #endregion


        #region auto-properties

        protected bool IsActing { get; set; }
        protected Action ActOnClick { get; set; }

        #endregion

        #region events

        public event EventHandler Clicked;

        #endregion

        #region ctor(s)

        public MaterialButton()
        {
            InitializeComponent();
        }

        #endregion

        #region access methods

        public void Configure(Action actOnClick)
        {
            ActOnClick = actOnClick;
        }

        public void SetIsLoading(bool isLoading)
        {
            IsLoading = isLoading;
        }

        public void ToggleIsLoading()
        {
            IsLoading = !IsLoading;
        }

        #endregion

        #region helper methods

        protected void UpdateActingUI()
        {
            MyContent.IsVisible = !IsActing;
            if (MustReduceToLoading) MyContent.WidthRequest = MyContent.HeightRequest = IsActing ? 0 : -1;
            if (MustReduceToLoading) ThisButton.Padding = IsActing ? new Thickness(ButtonPadding.Top) : ButtonPadding;
            MyLoading.IsVisible = IsActing;
        }

        protected void HandleButtonTapped(System.Object sender, System.EventArgs e)
        {
            Clicked?.Invoke(sender, e);
            if (!(ActOnClick is null)) ActOnClick();
        }

        #endregion
    }
}
