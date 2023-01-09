using System;
namespace MobileAppForms.Exceptions
{
    public class InsufficientCustomerDataException : Exception
    {
        #region ctor(s)

        public InsufficientCustomerDataException() : base() { }

        public InsufficientCustomerDataException(string message) : base(message) { }

        #endregion
    }
}

