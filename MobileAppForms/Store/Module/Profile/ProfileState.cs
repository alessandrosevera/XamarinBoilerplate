using System;
using MobileAppForms.Model;
using MobileAppForms.Model.Core;
using PropertyChanged;
using VuexSharp;

namespace MobileAppForms.Store
{
    [AddINotifyPropertyChangedInterface]
    public class ProfileState : State
    {
        #region auto-properties

        public Utilizer CurrentUtilizer { get; internal set; }
        public CustomerCredentials? CurrentCustomerCredentials { get; internal set; }
        public CustomerPreferences? CustomerPreferences { get; }

        #endregion

        #region ctor(s)

        public ProfileState(CustomerCredentials? currentCustomerCredentials, CustomerPreferences? customerPreferences)
        {
            CurrentUtilizer = new Anonymous();

            CustomerPreferences = customerPreferences;
            CurrentCustomerCredentials = currentCustomerCredentials;
        }

        public ProfileState(Utilizer currentUtilizer, CustomerCredentials? currentCustomerCredentials, CustomerPreferences? customerPreferences)
        {
            CurrentUtilizer = currentUtilizer;

            CustomerPreferences = customerPreferences;
            CurrentCustomerCredentials = currentCustomerCredentials;
        }

        #endregion

        #region immutable methods

        public ProfileState WithCustomerCredentials(CustomerCredentials? customerCredentials)
        {
            return new ProfileState(CurrentUtilizer, customerCredentials, CustomerPreferences);
        }

        public ProfileState WithCurrentUtilizer(Utilizer currentUtilizer)
        {
            return new ProfileState(currentUtilizer, CurrentCustomerCredentials, CustomerPreferences);
        }

        public ProfileState WithCustomerPreferences(CustomerPreferences? customerPreferences)
        {
            return new ProfileState(CurrentUtilizer, CurrentCustomerCredentials, customerPreferences);
        }

        #endregion
    }
}
