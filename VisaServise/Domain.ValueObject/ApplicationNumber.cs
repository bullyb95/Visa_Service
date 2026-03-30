using Domain.ValueObject.Base;
using Domain.ValueObject.Validators;

namespace Domain.ValueObject;

public class ApplicationNumber : ValueObject<ApplicationNumber>
{
    public string Value { get; }

    public ApplicationNumber(string number)
        : base(new ApplicationNumberValidator(), new ApplicationNumberValue(number))
    {
        Value = number;
    }

    private record ApplicationNumberValue(string Number);
}