using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MobileAppForms.Extension
{
    public static class ValidationExtension
    {
        #region extension methods

        public static bool IsValidEmail(this string email)
        {
            var isValid = true;
            try
            {
                var mailAddress = new MailAddress(email);
            }
            catch (Exception)
            {
                isValid = false;
            }

            return isValid;
        }

        public static bool IsValidMatch(this string inputString, string regEx)
        {
            return !String.IsNullOrEmpty(inputString) && Regex.IsMatch(inputString, regEx);
        }

        #endregion
    }
}
