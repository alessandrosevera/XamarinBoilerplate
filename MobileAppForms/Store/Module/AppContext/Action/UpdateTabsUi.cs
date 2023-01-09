using System.Threading.Tasks;
using MobileAppForms.Provider.Core;
using MobileAppForms.Vue;
using AppContext = VuexSharp.ActionContext<MobileAppForms.Store.AppContextState, MobileAppForms.Store.InternalState>;

namespace MobileAppForms.Store
{
    public class UpdateTabsUi : VuexSharp.Action<AppContextState, InternalState>
    {
        #region auto-properties

        private SharedRootComponent RootComponent { get; }

        #endregion

        #region ctor(s)

        public UpdateTabsUi(SharedRootComponent rootComponent)
        {
            RootComponent = rootComponent;
        }

        #endregion

        #region VuexSharp.Action implementation

        public Task Execute(AppContext context, object payload)
        {
            RootComponent?.UpdateTabsUi();

            return Task.CompletedTask;
        }

        #endregion
    }
}