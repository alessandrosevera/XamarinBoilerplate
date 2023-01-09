using System;
using VuexSharp;

namespace MobileAppForms.Store
{
    public class ProfileModule : ModuleBase<ProfileState, InternalState>
    {
        #region ctor(s)

        public ProfileModule(InternalStore store, MutationFactory mutationFactory, ActionFactory actionFactory)
            : base(store, mutationFactory, actionFactory)
        {
        }

        #endregion

        #region abstract methods implementation

        public override void CreateState()
        {
            State = new ProfileState(null, null);
        }

        public override void DisposeState()
        {
        }

        #endregion
    }
}
