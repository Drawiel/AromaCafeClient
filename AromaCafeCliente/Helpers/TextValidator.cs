using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AromaCafeCliente.Helpers
{
    public static class TextValidator
    {
        public static bool ValidateNameInput(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
                return false;
            string formattedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
            return name == formattedName;
        }

        public static bool ValidateEmailInput(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool ValidatePostalCode(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                return false;

            string pattern = @"^\d{5}(-\d{4})?$";
            return Regex.IsMatch(postalCode, pattern);
        }

        public static bool ValidateNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
