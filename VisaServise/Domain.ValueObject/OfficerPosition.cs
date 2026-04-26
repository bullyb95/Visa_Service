using Domain.ValueObject.Base;
using Domain.ValueObject.Validators;

namespace Domain.ValueObject;

public class OfficerPosition(string value)
    : ValueObject<string>(new OfficerPositionValidator(), value);