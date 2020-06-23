using System;
using System.Globalization;
using System.Windows.Controls;

namespace Laba6.Utils
{
    class NRule : ValidationRule
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
                return new ValidationResult(false, "Недопустимые символы. Step должно быть числом");
            }

            if (convertedValue <= 0)
            {
                return new ValidationResult(false, "Шаг не может быть меньше или равен 0.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
