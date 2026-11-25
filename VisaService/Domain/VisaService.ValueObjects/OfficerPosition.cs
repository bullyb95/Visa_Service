using VisaService.ValueObjects.Base;
using VisaService.ValueObjects.Validators;

namespace VisaService.ValueObjects;

public class OfficerPosition(string value)
    : ValueObject<string>(new OfficerPositionValidator(), value);
