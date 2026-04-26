using Domain.ValueObject.Base;
using Domain.ValueObject.Exceptions;

namespace Domain.ValueObject.Validators;

public class OfficerPositionValidator : IValidator<string>
{
    private readonly string[] _validPositions = { "Junior Officer", "Officer", "Senior Officer", "Chief Officer" };

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (!_validPositions.Contains(value))
            throw new FormatException($"Position must be one of: {string.Join(", ", _validPositions)}");
    }
}