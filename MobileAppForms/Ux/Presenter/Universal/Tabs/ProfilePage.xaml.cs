using System;
using System.Collections.Generic;
using MobileAppForms.Model;
using MobileAppForms.Model.Core;
using NGettext;
using VueSharp;
using VueSharp.Model.Core;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppForms.Ux
{
    public partial class ProfilePage : ContentPage, Presenter, OverlayHost, LocalizableElement, TabItemPage
    {
        #region auto-properties

        private View PageContent { get; set; }

        #endregion

        #region properties

        public AbsoluteLayout AbsoluteRoot => Root;
        public AbsoluteLayout AbsoluteContent => Root;
        public AbsoluteLayout OverlayContainer => SuperRoot;

        public AppTabs AppTab => AppTabs.Profile;

        #endregion

        #region ctor(s)

        public ProfilePage()
        {
            InitializeComponent();

            ActuatePageContent();
        }

        #endregion

        #region access methods

        public void Initialize()
        {
            InitializePageContent();
        }

        public void Configure()
        {
            ConfigurePageContent();
        }

        #endregion

        #region helper methods

        private void InitializePageContent()
        {
            if (PageContent is Phone.ProfilePageContent phonePageContent)
            {
                phonePageContent.Initialize();
            }
        }

        private void ConfigurePageContent()
        {
            if (PageContent is Phone.ProfilePageContent phonePageContent)
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
                    PageContent = new Phone.ProfilePageContent();
                    break;
                case nameof(DeviceIdiom.Phone):
                default:
                    PageContent = new Phone.ProfilePageContent();
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
