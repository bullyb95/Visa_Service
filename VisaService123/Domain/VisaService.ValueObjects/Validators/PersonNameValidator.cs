using VisaService.ValueObjects.Base;
using VisaService.ValueObjects.Exceptions;

namespace VisaService.ValueObjects.Validators;


public class PersonNameValidator : IValidator<string>
{
    private const int MinLength = 2;
    private const int MaxLength = 100;

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
