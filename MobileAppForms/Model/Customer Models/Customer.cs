using System;
using MobileAppForms.Model.Core;
using NGettext;
using VueSharp.Model.Core;

namespace MobileAppForms.Model
{
    public readonly struct Customer : Utilizer
    {
        #region auto-properties

        public string FirstName { get; }
        public string LastName { get; }

        public string DefaultInitials { get; }

        #endregion

        #region properties

        public string Initials => !string.IsNullOrEmpty(FirstName) && FirstName.Trim().Length > 0 && !string.IsNullOrEmpty(LastName) && LastName.Trim().Length > 0 ? string.Format("{0}{1}", FirstName[0], LastName[0])
            : !string.IsNullOrEmpty(FirstName) && FirstName.Trim().Length > 1 ? string.Format("{0}{1}", FirstName[0], FirstName[1])
            : !string.IsNullOrEmpty(LastName) && LastName.Trim().Length > 1 ? string.Format("{0}{1}", LastName[0], LastName[1])
            : DefaultInitials;

        #endregion

        #region ctor(s)

        public Customer(string firstName, string lastName, string defaultInitials)
        {
            FirstName = firstName;
            LastName = lastName;
            
            DefaultInitials = defaultInitials;
        }

        #endregion

        #region access methods

        public Anonymous ToAnonymous()
        {
            return new Anonymous();
        }

        #endregion
    }
}
