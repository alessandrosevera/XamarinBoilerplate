using System;
using System.Collections.Generic;
using NGettext;
using VueSharp.Model.Core;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Phone
{
    public partial class ProfilePageContent : Grid, LocalizableElement
    {
        #region ctor(s)

        public ProfilePageContent()
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
            NavBar.Title = catalog.GetString("Profilo");
        }

        #endregion
    }
}
