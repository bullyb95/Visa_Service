using Domain.ValueObject.Base;
using Domain.ValueObject.Exceptions;

namespace Domain.ValueObject.Validators;

public class EmailValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (!value.Contains('@') || !value.Contains('.'))
            throw new FormatException("Invalid email format");
    }
}

public class PhoneValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
    }
}