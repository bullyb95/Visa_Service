using VisaService.ValueObjects.Base;
using VisaService.ValueObjects.Exceptions;

namespace VisaService.ValueObjects.Validators;

public class EmailValidator : IValidator<string>
{
    private const int MaxLength = 254;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MaxLength)
            throw new ArgumentLongValueException(nameof(value), value, MaxLength);
        if (!value.Contains('@') || !value.Contains('.'))
            throw new FormatException($"Значение \"{value}\" не является корректным email адресом");
    }
}

public class PhoneValidator : IValidator<string>
{
    private const int MinLength = 7;
    private const int MaxLength = 20;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length < MinLength)
            throw new ArgumentShortValueException(nameof(value), value, MinLength);
        if (value.Length > MaxLength)
            throw new ArgumentLongValueException(nameof(value), value, MaxLength);
    }
}
