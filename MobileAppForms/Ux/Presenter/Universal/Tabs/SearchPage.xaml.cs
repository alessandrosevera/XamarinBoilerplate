using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MobileAppForms.Model;
using MobileAppForms.Model.Core;
using NGettext;
using PropertyChanged;
using SimpleTouchView;
using VueSharp;
using VueSharp.Model;
using VueSharp.Model.Core;
using Xamarin.Essentials;
using Xamarin.Forms;
using static MobileAppForms.Vue.SearchComponent;

namespace MobileAppForms.Ux
{
    public partial class SearchPage : PanelGesturePage, Presenter, OverlayHost, LocalizableElement, TabItemPage
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

        private bool IsBackdropInteractionActing { get; set; }
        private bool IsKnobInteractionActing { get; set; }

        #endregion

        #region properties

        public AbsoluteLayout AbsoluteRoot => Root;
        public AbsoluteLayout AbsoluteContent => Root;
        public AbsoluteLayout OverlayContainer => SuperRoot;

        public AppTabs AppTab => AppTabs.Search;

        protected override double AdditionalDraggableHeight => 40;
        protected override View PanelDraggableArea => null;
        protected override View Panel => null;

        #endregion

        #region ctor(s)

        public SearchPage()
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
            if (PageContent is Phone.SearchPageContent phonePageContent)
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
                    PageContent = new Phone.SearchPageContent();
                    break;
                case nameof(DeviceIdiom.Phone):
                default:
                    PageContent = new Phone.SearchPageContent();
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
