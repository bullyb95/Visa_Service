using Domain.ValueObject.Base;
using Domain.ValueObject.Validators;

namespace Domain.ValueObject;

public class FirstName(string value) : ValueObject<string>(new PersonNameValidator(), value);
public class LastName(string value) : ValueObject<string>(new PersonNameValidator(), value);

public class PersonName
{
    public FirstName FirstName { get; }
    public LastName LastName { get; }

    public PersonName(string firstName, string lastName)
    {
        FirstName = new FirstName(firstName);
        LastName = new LastName(lastName);
    }
}