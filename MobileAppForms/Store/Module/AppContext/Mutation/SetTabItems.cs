using System;
using System.Collections.Generic;
using MobileAppForms.Model;
using VuexSharp;

namespace MobileAppForms.Store
{
    public class SetTabItems : Mutation<AppContextState, Dictionary<AppTabs, TabItemState>>
    {
        #region Mutation implementation

        public AppContextState Apply(AppContextState state, Dictionary<AppTabs, TabItemState> tabItems)
        {
            return state.WithTabItems(tabItems);
        }

        #endregion
    }
}