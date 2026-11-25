using VisaService.ValueObjects.Base;
using VisaService.ValueObjects.Validators;

namespace VisaService.ValueObjects;

public class LastName(string value)
    : ValueObject<string>(new PersonNameValidator(), value);
