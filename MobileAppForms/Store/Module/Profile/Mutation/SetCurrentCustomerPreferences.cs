using System;
using MobileAppForms.Model;
using VuexSharp;

namespace MobileAppForms.Store
{
    public class SetCurrentCustomerPreferences : Mutation<ProfileState, CustomerPreferences>
    {
        #region Mutation implementation

        public ProfileState Apply(ProfileState state, CustomerPreferences customerPreferences)
        {
            return state.WithCustomerPreferences(customerPreferences);
        }


        #endregion
    }
}