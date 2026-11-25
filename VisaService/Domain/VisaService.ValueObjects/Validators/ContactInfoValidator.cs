using VisaService.ValueObjects.Base;
using VisaService.ValueObjects.Exceptions;

namespace VisaService.ValueObjects.Validators;

public class EmailValidator : IValidator<string>
{
    public const int MAX_LENGTH = 254;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
        if (!value.Contains('@') || !value.Contains('.'))
            throw new FormatException($"Значение \"{value}\" не является корректным email адресом");
    }
}

public class PhoneValidator : IValidator<string>
{
    public const int MIN_LENGTH = 7;
    public const int MAX_LENGTH = 20;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length < MIN_LENGTH)
            throw new ArgumentShortValueException(nameof(value), value, MIN_LENGTH);
        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}
