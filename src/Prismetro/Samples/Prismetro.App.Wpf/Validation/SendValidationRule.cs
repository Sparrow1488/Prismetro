using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace Prismetro.App.Wpf.Validation;

public class SendValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is string text && text.Any())
        {
            return new ValidationResult(true, null);
        }

        return new ValidationResult(false, "Введите что-нибудь!");
    }
}