using System;
using System.Threading.Tasks;
using MobileAppForms.Abstraction;
using VueSharp;
using VueSharp.Abstraction;

namespace MobileAppForms.Vue
{
    public class ProfileComponent : SharedTabComponent<bool>, AnonymousDeniedComponent
    {
        #region ctor(s)

        public ProfileComponent()
        {
        }

        #endregion

        #region abstract methods implementation

        protected override Task Configure(bool state)
        {
            return Task.CompletedTask;
        }

        protected override Task Initialize(bool state)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
