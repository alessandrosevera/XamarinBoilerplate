using System;
using System.Collections.Generic;
using MobileAppForms.Model;
using NGettext;
using VueSharp.Model.Core;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Phone
{
    public partial class FavoritesPageContent : Grid, LocalizableElement
    {
        #region fields

        private double _latestScrollY;

        #endregion

        #region auto-properties

        private ScrollDirection ScrollDirection { get; set; }

        #endregion

        #region ctor(s)

        public FavoritesPageContent()
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
            NavBar.Title = catalog.GetString("Locali preferiti");
        }

        #endregion

        #region event handlers

        private void HandleItemsListViewScrolled(System.Object sender, Xamarin.Forms.ScrolledEventArgs e)
        {
            double currentScrollY = e.ScrollY;
            if (currentScrollY < 0) currentScrollY = 0;

            if (currentScrollY > _latestScrollY)
            {
                ScrollDirection = ScrollDirection.Bottom;
            }
            else if (currentScrollY < _latestScrollY)
            {
                ScrollDirection = ScrollDirection.Top;
            }
            else
            {
                ScrollDirection = ScrollDirection.Idle;
            }

            _latestScrollY = currentScrollY;
            System.Diagnostics.Debug.WriteLine(currentScrollY);
        }

        #endregion
    }
}
