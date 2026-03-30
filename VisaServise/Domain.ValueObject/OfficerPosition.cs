using Domain.ValueObject.Base;
using Domain.ValueObject.Validators;

namespace Domain.ValueObject;

public class OfficerPosition : ValueObject<OfficerPosition>
{
    public string Value { get; }

    public OfficerPosition(string position)
        : base(new OfficerPositionValidator(), new OfficerPositionValue(position))
    {
        Value = position;
    }

    private record OfficerPositionValue(string Position);
}