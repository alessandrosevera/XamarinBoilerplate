using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGettext;
using VueSharp;
using VueSharp.Model.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppForms.Ux
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : TabbedPage, Presenter, OverlayHost, LocalizableElement
    {
        #region properties

        public SearchPage SearchPage => SearchTab;
        public FavoritesPage FavoritesPage => FavoritesTab;
        public AgencyPage AgencyPage => AgencyTab;
        public ProfilePage ProfilePage => ProfileTab;

        public AbsoluteLayout AbsoluteRoot => (this.CurrentPage as Presenter)?.AbsoluteRoot;
        public AbsoluteLayout AbsoluteContent => (this.CurrentPage as Presenter)?.AbsoluteContent;
        public AbsoluteLayout OverlayContainer => (this.CurrentPage as OverlayHost)?.OverlayContainer;

        private Action ActOnUpdateTabsUi { get; set; }

        #endregion

        #region ctor(s)

        public RootPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
        }

        #endregion

        #region access methods

        public void Configure(Action actOnUpdateTabsUi)
        {
            ActOnUpdateTabsUi = actOnUpdateTabsUi;
        }

        internal void RaiseUpdateTabsUi()
        {
            if (!(ActOnUpdateTabsUi is null)) ActOnUpdateTabsUi();
        }

        #endregion

        #region LocalizableElement implementation

        public void ApplyLocalization(ICatalog catalog)
        {
            SearchTab.Title = catalog.GetString("Cerca");
            FavoritesTab.Title = catalog.GetString("Preferiti");
            AgencyTab.Title = catalog.GetString("Agenzia");
            ProfileTab.Title = catalog.GetString("Profilo");
        }

        #endregion
    }
}
