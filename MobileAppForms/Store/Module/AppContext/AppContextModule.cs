using System;
using VuexSharp;

namespace MobileAppForms.Store
{
    public class AppContextModule : ModuleBase<AppContextState, InternalState>
    {
        #region ctor(s)

        public AppContextModule(InternalStore store, MutationFactory mutationFactory, ActionFactory actionFactory)
            : base(store, mutationFactory, actionFactory)
        {
        }

        #endregion

        #region abstract methods implementation

        public override void CreateState()
        {
            State = new AppContextState();
        }

        public override void DisposeState()
        {
        }

        #endregion
    }
}
