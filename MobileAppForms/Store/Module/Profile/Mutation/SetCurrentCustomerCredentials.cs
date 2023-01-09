using System;
using MobileAppForms.Model;
using VuexSharp;

namespace MobileAppForms.Store
{
    public class SetCurrentCustomerCredentials : Mutation<ProfileState, CustomerCredentials?>
    {
        #region Mutation implementation

        public ProfileState Apply(ProfileState state, CustomerCredentials? customerCredentials)
        {
            state.CurrentCustomerCredentials = customerCredentials;
            return state;
        }


        #endregion
    }
}
