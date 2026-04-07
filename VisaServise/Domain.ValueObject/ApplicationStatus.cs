using Domain.ValueObject.Base;
using Domain.ValueObject.Validators;

namespace Domain.ValueObject;

public class ApplicationStatus(string status)
    : ValueObject<string>(new ApplicationStatusValidator(), status);