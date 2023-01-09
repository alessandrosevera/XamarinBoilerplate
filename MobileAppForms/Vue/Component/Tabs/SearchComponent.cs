using System;
using System.Threading.Tasks;
using MobileAppForms.Abstraction;
using VueSharp;
using VueSharp.Abstraction;
using VueSharp.Service.Core;

namespace MobileAppForms.Vue
{
    public class SearchComponent : SharedTabComponent<bool>
    {
        #region auto-properties

        private SafeAreaInsetsService SafeAreaInsetsService { get; }

        #endregion

        #region ctor(s)

        public SearchComponent(SafeAreaInsetsService safeAreaInsetsService)
        {
            SafeAreaInsetsService = safeAreaInsetsService;
        }

        #endregion

        #region abstract methods implementation

        protected override Task Configure(bool state)
        {
            return Task.CompletedTask;
        }

        protected override Task Initialize(bool state)
        {
            var xx = SafeAreaInsetsService.GetSafeAreaInsets();
            return Task.CompletedTask;
        }

        #endregion
    }
}
