using System.ComponentModel.DataAnnotations;

namespace ClientePersona.Api.Validations
{
    public class NotPlaceholderAttribute : ValidationAttribute
    {
        private static readonly HashSet<string> InvalidValues = new(StringComparer.OrdinalIgnoreCase)
        {
            "string",
            "strings",
            "test",
            "prueba",
            "example",
            "ejemplo"
        };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string text)
            {
                return ValidationResult.Success;
            }

            var normalized = text.Trim();

            if (InvalidValues.Contains(normalized))
            {
                return new ValidationResult(ErrorMessage ?? $"El campo {validationContext.DisplayName} no puede usar un valor generico de ejemplo");
            }

            return ValidationResult.Success;
        }
    }
}
