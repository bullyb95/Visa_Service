using VisaService.ValueObjects.Base;
using VisaService.ValueObjects.Validators;

namespace VisaService.ValueObjects;

public class ApplicationNumber(string value)
    : ValueObject<string>(new ApplicationNumberValidator(), value);
