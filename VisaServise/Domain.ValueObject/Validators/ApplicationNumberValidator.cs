using Domain.ValueObject.Base;
using Domain.ValueObject.Exceptions;

namespace Domain.ValueObject.Validators;

public class ApplicationNumberValidator : IValidator<string>
{
    public static int LENGTH => 5;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length != LENGTH)
            throw new FormatException($"Application number must be exactly {LENGTH} characters");
    }
}