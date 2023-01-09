using System;
using MobileAppForms.Model.Core;
using VuexSharp;

namespace MobileAppForms.Store
{
    public class SetCurrentUtilizer : Mutation<ProfileState, Utilizer>
    {
        #region Mutation implementation

        public ProfileState Apply(ProfileState state, Utilizer utilizer)
        {
            state.CurrentUtilizer = utilizer;
            return state;
        }


        #endregion
    }
}
