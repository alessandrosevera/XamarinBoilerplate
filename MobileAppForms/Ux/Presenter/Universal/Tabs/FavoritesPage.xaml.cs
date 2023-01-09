using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MobileAppForms.Model;
using MobileAppForms.Model.Core;
using NGettext;
using PropertyChanged;
using VueSharp;
using VueSharp.Model.Core;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppForms.Ux
{
    public partial class FavoritesPage : ContentPage, Presenter, OverlayHost, LocalizableElement, TabItemPage
    {
        #region nested classes

        [AddINotifyPropertyChangedInterface]
        public class Context
        {
        }

        #endregion

        #region auto-properties

        private Context OwnContext { get; set; }
        private View PageContent { get; set; }

        #endregion

        #region properties

        public AbsoluteLayout AbsoluteRoot => Root;
        public AbsoluteLayout AbsoluteContent => Root;
        public AbsoluteLayout OverlayContainer => SuperRoot;

        public AppTabs AppTab => AppTabs.Favorites;

        #endregion

        #region ctor(s)

        public FavoritesPage()
        {
            InitializeComponent();

            ActuatePageContent();

            OwnContext = new Context();
            BindingContext = OwnContext;
        }

        #endregion

        #region access methods

        public void Configure()
        {
            ConfigurePageContent();
        }

        public void Initialize()
        {
        }

        #endregion

        #region helper methods

        private void ConfigurePageContent()
        {
            if (PageContent is Phone.FavoritesPageContent phonePageContent)
            {
                phonePageContent.Configure();
            }
        }

        private void ActuatePageContent()
        {
            switch (DeviceInfo.Idiom.ToString())
            {
                case nameof(DeviceIdiom.Tablet):
                    // @AS TABLET
                    PageContent = new Phone.FavoritesPageContent();
                    break;
                case nameof(DeviceIdiom.Phone):
                default:
                    PageContent = new Phone.FavoritesPageContent();
                    break;
            }

            if (!(PageContent is null))
            {
                PageContent.IsVisible = false;
                AbsoluteLayout.SetLayoutFlags(PageContent, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(PageContent, new Rectangle(0, 0, 1, 1));
                Root.Children.Add(PageContent);
                Root.LowerChild(PageContent);
                PageContent.IsVisible = true;
            }
        }

        #endregion

        #region LocalizableElement implementation

        public void ApplyLocalization(ICatalog catalog)
        {
            if (PageContent is LocalizableElement localizable)
            {
                localizable.ApplyLocalization(catalog);
            }
        }

        #endregion

    }
}
