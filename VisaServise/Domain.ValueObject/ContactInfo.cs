using Domain.ValueObject.Base;
using Domain.ValueObject.Validators;

namespace Domain.ValueObject;

public class ContactInfo : ValueObject<ContactInfo>
{
    public string? Email { get; }
    public string? Phone { get; }
    public string? Address { get; }

    public ContactInfo(string? email = null, string? phone = null, string? address = null)
        : base(new ContactInfoValidator(), new ContactInfoValue(email, phone, address))
    {
        Email = email;
        Phone = phone;
        Address = address;
    }

    private record ContactInfoValue(string? Email, string? Phone, string? Address);
}