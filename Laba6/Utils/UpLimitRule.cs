using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Laba6.Utils
{

    [ContentProperty("Wrapper")]
    class UpLimitRule : ValidationRule
    {

        public Wrapper Wrapper { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double convertedValue;
            try
            {
                convertedValue = Double.Parse((string)value, new NumberFormatInfo());
            }
            catch
            {
                return new ValidationResult(false, "Недопустимые символы. XStop должно быть целым числом.");
            }

            if (convertedValue < Wrapper.lowLimit)
            {

               return new ValidationResult(false, "Конечное значение не может быть меньше начального значения аргумента.");
            }

            return ValidationResult.ValidResult;
        }
    }
}

