using Domain.ValueObject.Base;
using Domain.ValueObject.Validators;

namespace Domain.ValueObject;

public class Email(string value) : ValueObject<string>(new EmailValidator(), value);
public class Phone(string value) : ValueObject<string>(new PhoneValidator(), value);

public class ContactInfo
{
    public Email Email { get; }
    public Phone Phone { get; }

    public ContactInfo(string email, string phone)
    {
        Email = new Email(email);
        Phone = new Phone(phone);
    }
}