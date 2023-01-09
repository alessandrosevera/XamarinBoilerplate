using System;
using MobileAppForms.Ux.Phone;
using VueSharp;
using VueSharp.Abstraction;

namespace MobileAppForms.Vue
{
    public class ErrorSnackbarComponent : SnackbarComponent
    {
        #region abstract methods implementation

        protected override Presenter CreatePresenter()
        {
            return new ErrorSnackbarPresenter();
        }

        #endregion
    }
}