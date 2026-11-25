using VisaService.ValueObjects.Base;
using VisaService.ValueObjects.Validators;

namespace VisaService.ValueObjects;

public class Phone(string value)
    : ValueObject<string>(new PhoneValidator(), value);
