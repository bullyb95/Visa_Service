using Domain.ValueObject.Base;
using Domain.ValueObject.Validators;

namespace Domain.ValueObject;

public class PersonName : ValueObject<PersonName>
{
    public string LastName { get; }
    public string FirstName { get; }
    public string? MiddleName { get; }

    public PersonName(string lastName, string firstName, string? middleName = null)
        : base(new PersonNameValidator(), new PersonNameValue(lastName, firstName, middleName))
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
    }

    private record PersonNameValue(string LastName, string FirstName, string? MiddleName);
}