using Domain.ValueObject.Base;
using Domain.ValueObject.Validators;

namespace Domain.ValueObject;

public class ApplicationNumber(string number)
    : ValueObject<string>(new ApplicationNumberValidator(), number);