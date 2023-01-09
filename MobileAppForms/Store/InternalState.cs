using System;
using VuexSharp;

namespace MobileAppForms.Store
{
    public class InternalState : RootState
    {

        #region auto-properties

        public InternalStore Store { get; }

        #endregion

        #region properties 

        public AppContextState AppContextState => Store.AppContextModule.State;
        public ProfileState ProfileState => Store.ProfileModule.State;

        #endregion

        #region ctor(s)

        public InternalState(InternalStore store)
        {
            Store = store;
        }

        #endregion
    }
}
