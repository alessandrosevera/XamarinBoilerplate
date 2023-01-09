using System;
namespace MobileAppForms.Model
{
    public readonly struct CustomerCredentials
    {
        #region auto-properties

        public string CustomerId { get; }
        public string Email { get; }

        public string BearerToken { get; }

        #endregion

        #region ctor(s)

        public CustomerCredentials(string customerId, string email, string bearerToken)
        {
            CustomerId = customerId;
            Email = email;
            BearerToken = bearerToken;
        }

        #endregion

        #region access methods

        public CustomerCredentials WithBearer(string bearerToken)
        {
            return new CustomerCredentials(CustomerId, Email, bearerToken);
        }

        public CustomerCredentials WithCustomerId(string customerId)
        {
            return new CustomerCredentials(customerId, Email, BearerToken);
        }

        public CustomerCredentials WithEmail(string email)
        {
            return new CustomerCredentials(CustomerId, email, BearerToken);
        }

        #endregion
    }
}
