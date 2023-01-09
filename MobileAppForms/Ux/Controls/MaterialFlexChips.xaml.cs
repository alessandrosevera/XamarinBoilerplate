using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PropertyChanged;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Controls
{
    public class ChipSelectionChangedEventArgs : EventArgs
    {
        public IEnumerable<object> SelectedItems { get; }

        public ChipSelectionChangedEventArgs(IEnumerable<object> selectedItems) : base()
        {
            SelectedItems = selectedItems;
        }
    }

    public partial class MaterialFlexChips : FlexLayout
    {
        #region nested classes

        public interface ChipDefinition
        {
            string Id { get; }
            string Label { get; }
        }

        [AddINotifyPropertyChangedInterface]
        internal class Context
        {
            #region auto-properties

            public ObservableCollection<SelectableChip> Items { get; set; }

            #endregion

            #region ctor(s)

            public Context()
            {
                Items = new ObservableCollection<SelectableChip>();
            }

            #endregion
        }

        [AddINotifyPropertyChangedInterface]
        internal class SelectableChip
        {
            #region auto-properties

            public int Index { get; }

            [AlsoNotifyFor(nameof(TextColor), nameof(BorderColor), nameof(BorderThickness), nameof(FontFamily))]
            public bool IsSelected { get; set; }

            [AlsoNotifyFor(nameof(Label), nameof(Id))]
            public object Tag { get; set; }

            public double Width { get; internal set; }
            public double FontSize { get; internal set; }

            [AlsoNotifyFor(nameof(NormalizedSpacing))]
            public Thickness Spacing { get; internal set; }

            [AlsoNotifyFor(nameof(NormalizedSpacing))]
            public bool IsFirst { get; internal set; }

            [AlsoNotifyFor(nameof(NormalizedSpacing))]
            public bool IsLast { get; internal set; }

            #endregion

            #region properties

            public string Id => Tag is string ? (string)Tag : Tag is ChipDefinition chipDefinition ? chipDefinition.Id : string.Empty;
            public string Label => Tag is string ? (string)Tag : Tag is ChipDefinition chipDefinition ? chipDefinition.Label : string.Empty;

            public string FontFamily => IsSelected ? _selectedFontFamily : _defaultFontFamily;
            public Color TextColor => IsSelected ? _selectedTextColor : _defaultTextColor;
            public Color BorderColor => IsSelected ? _selectedBorderColor : _defaultBorderColor;
            public Color BackgroundColor => IsSelected ? _selectedBackgroundColor : _defaultBackgroundColor;
            public int BorderThickness => IsSelected ? _selectedBorderThickness : _defaultBorderThickness;

            public Thickness NormalizedSpacing => IsFirst && IsLast ? new Thickness(0, Spacing.Top, 0, Spacing.Bottom)
                : IsFirst ? new Thickness(0, Spacing.Top, Spacing.Right, Spacing.Bottom)
                : IsLast ? new Thickness(Spacing.Left, Spacing.Top, 0, Spacing.Bottom)
                : Spacing;

            #endregion

            #region fields

            private string _defaultFontFamily { get; set; }
            private string _selectedFontFamily { get; set; }
            private Color _defaultTextColor { get; set; }
            private Color _selectedTextColor { get; set; }
            private Color _defaultBorderColor { get; set; }
            private Color _selectedBorderColor { get; set; }
            private Color _defaultBackgroundColor { get; set; }
            private Color _selectedBackgroundColor { get; set; }
            private int _defaultBorderThickness { get; set; }
            private int _selectedBorderThickness { get; set; }

            #endregion

            #region ctor(s)

            public SelectableChip(int index, object tag)
            {
                Index = index;
                Tag = tag;
            }

            #endregion

            #region immutable methods

            public SelectableChip WithStyles(string fontFamily, string selectedFontFamily, Color textColor, Color selectedTextColor,
                Color borderColor, Color selectedBorderColor, Color backgroundColor, Color selectedBackgroundColor,
                int borderThickness, int selectedBorderThickness, double fontSize, Thickness spacing)
            {

                var chip = new SelectableChip(Index, Tag)
                {
                    _defaultFontFamily = fontFamily,
                    _defaultTextColor = textColor,
                    _defaultBorderColor = borderColor,
                    _defaultBackgroundColor = backgroundColor,
                    _defaultBorderThickness = borderThickness,
                    _selectedFontFamily = selectedFontFamily,
                    _selectedTextColor = selectedTextColor,
                    _selectedBorderColor = selectedBorderColor,
                    _selectedBackgroundColor = selectedBackgroundColor,
                    _selectedBorderThickness = selectedBorderThickness
                };

                chip.Spacing = spacing;
                chip.FontSize = fontSize;
                chip.IsSelected = IsSelected;
                chip.Width = Width;
                chip.IsFirst = IsFirst;
                chip.IsLast = IsLast;

                return chip;
            }

            #endregion
        }

        #endregion

        #region bindable properties

        public static BindableProperty SelectionModeProperty = BindableProperty.Create(nameof(SelectionMode), typeof(SelectionMode), typeof(MaterialFlexChips), defaultValue: SelectionMode.Single);

        public static BindableProperty MaximumLabelLengthThresholdForWidthProperty = BindableProperty.Create(nameof(MaximumLabelLengthThresholdForWidth), typeof(int), typeof(MaterialFlexChips), defaultValue: -1,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnLayoutPropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static BindableProperty ItemWidthProperty = BindableProperty.Create(nameof(ItemWidth), typeof(double), typeof(MaterialFlexChips), defaultValue: -1d,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnLayoutPropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialFlexChips), defaultValue: Font.Default.FontFamily,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnStylePropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static BindableProperty SelectedFontFamilyProperty = BindableProperty.Create(nameof(SelectedFontFamily), typeof(string), typeof(MaterialFlexChips), defaultValue: Font.Default.FontFamily,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnStylePropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialFlexChips), defaultValue: Color.Gray,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnStylePropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static BindableProperty SelectedTextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialFlexChips), defaultValue: Color.White,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnStylePropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialFlexChips), defaultValue: Color.Transparent,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnStylePropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(MaterialFlexChips), defaultValue: Color.Gray,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnStylePropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(MaterialFlexChips), defaultValue: 1,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnStylePropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static BindableProperty SelectedBorderThicknessProperty = BindableProperty.Create(nameof(SelectedBorderThickness), typeof(int), typeof(MaterialFlexChips), defaultValue: 2,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnStylePropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialFlexChips), defaultValue: Color.Gray,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnStylePropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static BindableProperty SelectedBorderColorProperty = BindableProperty.Create(nameof(SelectedBorderColor), typeof(Color), typeof(MaterialFlexChips), defaultValue: Color.Gray,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnStylePropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialFlexChips), defaultValue: 13d,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnStylePropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        public static BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(Thickness), typeof(MaterialFlexChips), defaultValue: new Thickness(0, 0, 10, 7),
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is MaterialFlexChips materialFlexChips)
                {
                    materialFlexChips.OnStylePropertyChanged();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[MaterialFlexChips] bindable element is wrong type!");
                }
            });

        #endregion

        #region properties

        public SelectionMode SelectionMode
        {
            get => (SelectionMode)GetValue(SelectionModeProperty);
            set => SetValue(SelectionModeProperty, value);
        }

        public int MaximumLabelLengthThresholdForWidth
        {
            get => (int)GetValue(MaximumLabelLengthThresholdForWidthProperty);
            set => SetValue(MaximumLabelLengthThresholdForWidthProperty, value);
        }

        public double ItemWidth
        {
            get => (double)GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public string SelectedFontFamily
        {
            get => (string)GetValue(SelectedFontFamilyProperty);
            set => SetValue(SelectedFontFamilyProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public Color SelectedTextColor
        {
            get => (Color)GetValue(SelectedTextColorProperty);
            set => SetValue(SelectedTextColorProperty, value);
        }

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public Color SelectedBackgroundColor
        {
            get => (Color)GetValue(SelectedBackgroundColorProperty);
            set => SetValue(SelectedBackgroundColorProperty, value);
        }

        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public int SelectedBorderThickness
        {
            get => (int)GetValue(SelectedBorderThicknessProperty);
            set => SetValue(SelectedBorderThicknessProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public Color SelectedBorderColor
        {
            get => (Color)GetValue(SelectedBorderColorProperty);
            set => SetValue(SelectedBorderColorProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public Thickness Spacing
        {
            get => (Thickness)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        #endregion

        #region auto-properties

        private Context OwnContext { get; set; }

        private Action<IEnumerable<object>> ActOnValueChanged { get; set; }

        #endregion

        #region events

        public event EventHandler<ChipSelectionChangedEventArgs> Changed;

        #endregion

        #region ctor(s)

        public MaterialFlexChips()
        {
            InitializeComponent();

            OwnContext = new Context();
            BindingContext = OwnContext;
        }

        #endregion

        #region access methods

        public void Configure(Action<IEnumerable<object>> actOnValueChanged)
        {
            ActOnValueChanged = actOnValueChanged;
        }

        public void Initialize(IEnumerable enumerable)
        {
            List<SelectableChip> selectableChips = new List<SelectableChip>();
            var enumerator = enumerable?.GetEnumerator();
            if (!(enumerator is null))
            {
                int index = 0;
                while (enumerator.MoveNext())
                {
                    var selectableChip = new SelectableChip(index, enumerator.Current);
                    selectableChip.Width = ItemWidth;

                    if (ItemWidth != -1 && MaximumLabelLengthThresholdForWidth != -1
                        && !string.IsNullOrEmpty(selectableChip.Label) && selectableChip.Label.Length > MaximumLabelLengthThresholdForWidth)
                    {
                        selectableChip.Width = -1;
                    }

                    selectableChip = selectableChip.WithStyles(FontFamily, SelectedFontFamily,
                        TextColor, SelectedTextColor, BorderColor, SelectedBorderColor, BackgroundColor, SelectedBackgroundColor,
                        BorderThickness, SelectedBorderThickness, FontSize, Spacing);

                    selectableChips.Add(selectableChip);
                    index++;
                }
            }
            if (selectableChips.Count > 0)
            {
                selectableChips.First().IsFirst = true;
                selectableChips.Last().IsLast = true;
            }
            OwnContext.Items = new ObservableCollection<SelectableChip>(selectableChips);
        }

        public IEnumerable<object> GetSelectedItems()
        {
            return OwnContext.Items.Where(i => !(i.Tag is null) && i.IsSelected).Select(i => i.Tag).ToList();
        }

        public IEnumerable<T> GetSelectedItems<T>()
        {
            return OwnContext.Items.Where(i => !(i.Tag is null) && i.Tag is T && i.IsSelected)
                .Select(i => i.Tag)
                .Cast<T>().ToList();
        }

        public void SelectItem(string itemToSelect)
        {
            OwnContext.Items.Select(item =>
            {
                item.IsSelected = !string.IsNullOrEmpty(item.Id) && !string.IsNullOrEmpty(itemToSelect) && item.Id == itemToSelect && SelectionMode != SelectionMode.None;
                return item;
            }).ToList();
        }

        public void DeselectItem(string itemToSelect)
        {
            OwnContext.Items.Select(item =>
            {
                if (!string.IsNullOrEmpty(item.Id) && !string.IsNullOrEmpty(itemToSelect) && item.Id == itemToSelect)
                {
                    item.IsSelected = false;
                }
                return item;
            }).ToList();
        }

        public void SelectItems(IEnumerable<string> itemsToSelect)
        {
            if (SelectionMode == SelectionMode.Single && itemsToSelect.Count() > 1)
            {
                SelectItem(itemsToSelect.First());
            }
            else
            {
                OwnContext.Items.Select(item =>
                {
                    item.IsSelected = !string.IsNullOrEmpty(item.Id) && !(itemsToSelect is null)
                        && itemsToSelect.Contains(item.Id) && SelectionMode != SelectionMode.None;
                    return item;
                }).ToList();
            }
        }

        public void SelectItem(ChipDefinition itemToSelect)
        {
            OwnContext.Items.Select(item =>
            {
                item.IsSelected = !string.IsNullOrEmpty(item.Id) && !(itemToSelect is null)
                    && ((item is ChipDefinition chipDefinition && chipDefinition.Id == itemToSelect.Id) || (item is SelectableChip selectableChip && selectableChip.Id == itemToSelect.Id))
                    && SelectionMode != SelectionMode.None;
                return item;

            }).ToList();
        }

        public void DeselectItem(ChipDefinition itemToSelect)
        {
            OwnContext.Items.Select(item =>
            {
                if (!string.IsNullOrEmpty(item.Id) && !(itemToSelect is null)
                    && ((item is ChipDefinition chipDefinition && chipDefinition.Id == itemToSelect.Id) || (item is SelectableChip selectableChip && selectableChip.Id == itemToSelect.Id)))
                {
                    item.IsSelected = false;
                }
                return item;

            }).ToList();
        }

        public void SelectItems(IEnumerable<ChipDefinition> itemsToSelect)
        {
            if (SelectionMode == SelectionMode.Single && itemsToSelect.Count() > 1)
            {
                SelectItem(itemsToSelect.First());
            }
            else
            {
                OwnContext.Items.Select(item =>
                {
                    item.IsSelected = !string.IsNullOrEmpty(item.Id) && !(itemsToSelect is null)
                        && ((item is ChipDefinition chipDefinition && itemsToSelect.Any(i => i.Id == chipDefinition.Id)) || (item is SelectableChip selectableChip && itemsToSelect.Any(i => i.Id == selectableChip.Id)))
                        && SelectionMode != SelectionMode.None;
                    return item;

                }).ToList();
            }
        }

        #endregion

        #region event handlers

        private void HandleChipTappedInternal(System.Object sender, System.EventArgs e)
        {
            if (e is TappedEventArgs tappedEventArgs && !(tappedEventArgs.Parameter is null) && tappedEventArgs.Parameter is SelectableChip chip)
            {
                bool isTargetSelected = !chip.IsSelected;
                switch(SelectionMode)
                {
                    case SelectionMode.Single:
                        OwnContext.Items.Select(i =>
                        {
                            if (i.Index == chip.Index)
                            {
                                i.IsSelected = isTargetSelected;
                            }
                            else
                            {
                                i.IsSelected = false;
                            }
                            return i;
                        }).ToList();
                        break;
                    case SelectionMode.Multiple:
                        chip.IsSelected = isTargetSelected;
                        break;
                }

                var newSelectedItems = GetSelectedItems();
                if (!(ActOnValueChanged is null)) ActOnValueChanged(newSelectedItems);
                Changed?.Invoke(this, new ChipSelectionChangedEventArgs(newSelectedItems));
            }
        }

        [SuppressPropertyChangedWarnings]
        internal void OnLayoutPropertyChanged()
        {
            if (!(OwnContext is null) && !(OwnContext.Items is null) && OwnContext.Items.Any())
            {
                OwnContext.Items.Select(i => {
                    double newWidth = (double)ItemWidth;
                    i.Width = newWidth;

                    if (newWidth != -1 && MaximumLabelLengthThresholdForWidth != -1
                        && !string.IsNullOrEmpty(i.Label) && i.Label.Length > MaximumLabelLengthThresholdForWidth)
                    {
                        i.Width = -1;
                    }
                    return i;

                }).ToList();
            }
        }

        [SuppressPropertyChangedWarnings]
        internal void OnStylePropertyChanged()
        {
            if (OwnContext.Items is null || !OwnContext.Items.Any()) return;

            OwnContext.Items.Select(item => item.WithStyles(FontFamily, SelectedFontFamily,
                        TextColor, SelectedTextColor, BorderColor, SelectedBorderColor, BackgroundColor, SelectedBackgroundColor,
                        BorderThickness, SelectedBorderThickness, FontSize, Spacing)).ToList();
        }

        #endregion
    }
}
