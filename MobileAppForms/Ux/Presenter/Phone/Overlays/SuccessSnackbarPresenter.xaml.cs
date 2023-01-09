﻿using System;
using System.Collections.Generic;
using VueSharp.Abstraction;
using Xamarin.Forms;

namespace MobileAppForms.Ux.Phone
{
    public partial class SuccessSnackbarPresenter : SnackbarPresenter
    {
        #region properties

        public override AbsoluteLayout AbsoluteContent => this;

        #endregion

        #region ctor(s)

        public SuccessSnackbarPresenter()
        {
            InitializeComponent();
        }

        #endregion

        #region access methods

        public override void Initialize(string text)
        {
            SnackbarLabel.Text = text;
        }

        #endregion
    }
}
