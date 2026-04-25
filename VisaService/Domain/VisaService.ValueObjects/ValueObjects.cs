using VisaService.ValueObjects.Base;
using VisaService.ValueObjects.Validators;

namespace VisaService.ValueObjects;

public class FirstName(string value)
    : ValueObject<string>(new PersonNameValidator(), value);

public class LastName(string value)
    : ValueObject<string>(new PersonNameValidator(), value);

public class MiddleName(string value)
    : ValueObject<string>(new PersonNameValidator(), value);

public class ApplicationNumber(string value)
    : ValueObject<string>(new ApplicationNumberValidator(), value);

public class Email(string value)
    : ValueObject<string>(new EmailValidator(), value);

public class Phone(string value)
    : ValueObject<string>(new PhoneValidator(), value);
