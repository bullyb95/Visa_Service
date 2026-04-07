using Domain.ValueObject.Base;
using Domain.ValueObject.Exceptions;

namespace Domain.ValueObject.Validators;

public class ApplicationStatusValidator : IValidator<string>
{
    private readonly string[] _validStatuses = { "Draft", "Submitted", "UnderReview", "Approved", "Rejected" };

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (!_validStatuses.Contains(value))
            throw new FormatException($"Status must be one of: {string.Join(", ", _validStatuses)}");
    }
}