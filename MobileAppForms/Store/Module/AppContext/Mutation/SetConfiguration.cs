using System;
using MobileAppForms.Model;
using VuexSharp;

namespace MobileAppForms.Store
{
    public class SetConfiguration : Mutation<AppContextState, Configuration>
    {
        #region Mutation implementation

        public AppContextState Apply(AppContextState state, Configuration configuration)
        {
            return state.WithConfiguration(configuration);
        }

        #endregion
    }
}