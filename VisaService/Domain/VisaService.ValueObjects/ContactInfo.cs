using VisaService.ValueObjects.Validators;

namespace VisaService.ValueObjects;

public class ContactInfo
{
    public Email Email { get; }
    public Phone Phone { get; }

    public ContactInfo(string email, string phone)
    {
        Email = new Email(email);
        Phone = new Phone(phone);
    }

    protected ContactInfo() { Email = default!; Phone = default!; }
}
 