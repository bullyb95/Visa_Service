using VisaService.ValueObjects.Base;
using VisaService.ValueObjects.Validators;

namespace VisaService.ValueObjects;

public class Email(string value)
    : ValueObject<string>(new EmailValidator(), value);
