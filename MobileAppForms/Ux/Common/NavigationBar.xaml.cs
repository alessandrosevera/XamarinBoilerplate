using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Common
{
    public partial class NavigationBar : Grid
    {
        #region bindable properties

        public static BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(NavigationBar), defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) => {

                if (bindable is NavigationBar navBar)
                {
                    navBar.TitleLabel.Text = (string)newVal;
                }
            });

        public static BindableProperty TitleStyleProperty = BindableProperty.Create(nameof(TitleStyle), typeof(Style), typeof(NavigationBar), defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) => {

                if (bindable is NavigationBar navBar)
                {
                    navBar.TitleLabel.Style = (Style)newVal;
                }
            });

        public static BindableProperty LeftActionIconProperty = BindableProperty.Create(nameof(LeftActionIcon), typeof(ImageSource), typeof(NavigationBar), defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) => {

                if (bindable is NavigationBar navBar)
                {
                    navBar.LeftActionFrame.IsVisible = !(newVal is null);
                    navBar.LeftIconImage.Source = !(newVal is null) ? (ImageSource)newVal : null;
                }
            });

        public static BindableProperty RightActionIconProperty = BindableProperty.Create(nameof(RightActionIcon), typeof(ImageSource), typeof(NavigationBar), defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) => {

                if (bindable is NavigationBar navBar)
                {
                    navBar.RightActionFrame.IsVisible = !(newVal is null);
                    navBar.RightIconImage.Source = !(newVal is null) ? (ImageSource)newVal : null;
                }
            });

        public static BindableProperty RightSecondaryActionIconProperty = BindableProperty.Create(nameof(RightSecondaryActionIcon), typeof(ImageSource), typeof(NavigationBar), defaultValue: null,
            propertyChanged: (bindable, oldVal, newVal) => {

                if (bindable is NavigationBar navBar)
                {
                    if (!(newVal is null))
                    {
                        Grid.SetColumn(navBar.TitleLabel, 2);
                        Grid.SetColumnSpan(navBar.TitleLabel, 1);
                    }

                    navBar.RightSecondaryActionFrame.IsVisible = !(newVal is null);
                    navBar.RightSecondaryIconImage.Source = !(newVal is null) ? (ImageSource)newVal : null;
                }
            });

        #endregion

        #region properties

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public Style TitleStyle
        {
            get => (Style)GetValue(TitleStyleProperty);
            set => SetValue(TitleStyleProperty, value);
        }

        public ImageSource LeftActionIcon
        {
            get => (ImageSource)GetValue(LeftActionIconProperty);
            set => SetValue(LeftActionIconProperty, value);
        }

        public ImageSource RightActionIcon
        {
            get => (ImageSource)GetValue(RightActionIconProperty);
            set => SetValue(RightActionIconProperty, value);
        }

        public ImageSource RightSecondaryActionIcon
        {
            get => (ImageSource)GetValue(RightSecondaryActionIconProperty);
            set => SetValue(RightSecondaryActionIconProperty, value);
        }

        #endregion

        #region auto-properties

        private Action ActOnLeftActionTapped { get; set; }
        private Action ActOnRightActionTapped { get; set; }
        private Action ActOnRightSecondaryActionTapped { get; set; }

        #endregion

        #region events

        public event EventHandler LeftActionTapped;
        public event EventHandler RightActionTapped;
        public event EventHandler RightSecondaryActionTapped;

        #endregion

        #region ctor(s)

        public NavigationBar()
        {
            InitializeComponent();
        }

        #endregion

        #region access methods

        public void Configure(Action actOnLeftActionTapped, Action actOnRightActionTapped, Action actOnRightSecondaryActionTapped)
        {
            ActOnLeftActionTapped = actOnLeftActionTapped;
            ActOnRightActionTapped = actOnRightActionTapped;
            ActOnRightSecondaryActionTapped = actOnRightSecondaryActionTapped;
        }

        #endregion

        #region event handlers

        private void HandleLeftActionIconTapped(System.Object sender, System.EventArgs e)
        {
            if (!(ActOnLeftActionTapped is null))
            {
                ActOnLeftActionTapped();
            }
            else
            {
                LeftActionTapped?.Invoke(sender, e);
            }
        }

        private void HandleRightActionIconTapped(System.Object sender, System.EventArgs e)
        {
            if (!(ActOnRightActionTapped is null))
            {
                ActOnRightActionTapped();
            }
            else
            {
                RightActionTapped?.Invoke(sender, e);
            }
        }

        private void HandleRightSecondaryActionIconTapped(System.Object sender, System.EventArgs e)
        {
            if (!(ActOnRightSecondaryActionTapped is null))
            {
                ActOnRightSecondaryActionTapped();
            }
            else
            {
                RightSecondaryActionTapped?.Invoke(sender, e);
            }
        }

        #endregion
    }
}
