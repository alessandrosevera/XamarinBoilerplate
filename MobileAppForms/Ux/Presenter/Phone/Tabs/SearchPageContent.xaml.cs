using System;
using System.Collections.Generic;
using MobileAppForms.Model;
using NGettext;
using VueSharp.Model.Core;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Phone
{
    public partial class SearchPageContent : Grid, LocalizableElement
    {
        #region ctor(s)

        public SearchPageContent()
        {
            InitializeComponent();
        }

        #endregion

        #region access methods

        public void Configure()
        {

        }

        public void Initialize()
        {

        }

        #endregion

        #region LocalizableElement implementation

        public void ApplyLocalization(ICatalog catalog)
        {
            NavBar.Title = catalog.GetString("Lista locali");
        }

        #endregion
    }
}
