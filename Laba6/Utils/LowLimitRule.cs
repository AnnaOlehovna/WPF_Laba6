using System;
using System.Globalization;
using System.Windows.Controls;

namespace Laba6.Utils
{
    class LowLimitRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double convertedValue;

            try
            {
                convertedValue = Double.Parse((string)value, new NumberFormatInfo());
            }
            catch
            {
                return new ValidationResult(false, "Недопустимые символы");
            }      
            return ValidationResult.ValidResult;
        }
    }
}
