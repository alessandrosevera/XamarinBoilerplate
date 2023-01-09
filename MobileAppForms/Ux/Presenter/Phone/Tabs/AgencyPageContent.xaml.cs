using System;
using System.Collections.Generic;
using NGettext;
using VueSharp.Model.Core;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Phone
{
    public partial class AgencyPageContent : Grid, LocalizableElement
    {
        #region ctor(s)

        public AgencyPageContent()
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
            NavBar.Title = catalog.GetString("Agenzia");
        }

        #endregion
    }
}
