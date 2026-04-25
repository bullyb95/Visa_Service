using VisaService.ValueObjects;
using VisaService.ValueObjects.Base;
using VisaService.Domain.Exceptions;

namespace VisaService.Domain;

public class Applicant : Entity<Guid>
{
    public LastName LastName { get; private set; }
    public FirstName FirstName { get; private set; }
    public MiddleName MiddleName { get; private set; }
    public ContactInfo ContactInfo { get; private set; }

    private readonly List<VisaApplication> _applications = new();
    public IReadOnlyCollection<VisaApplication> Applications => _applications.AsReadOnly();


    protected Applicant() : base()
    {
        LastName = default!;
        FirstName = default!;
        MiddleName = default!;
        ContactInfo = default!;
    }

    public Applicant(Guid id, LastName lastName, FirstName firstName, MiddleName middleName, ContactInfo contactInfo)
        : base(id)
    {
        LastName = lastName ?? throw new ArgumentNullValueException(nameof(lastName));
        FirstName = firstName ?? throw new ArgumentNullValueException(nameof(firstName));
        MiddleName = middleName ?? throw new ArgumentNullValueException(nameof(middleName));
        ContactInfo = contactInfo ?? throw new ArgumentNullValueException(nameof(contactInfo));
    }

    public VisaApplication CreateApplication(ApplicationNumber number)
    {
        if (number == null) throw new ArgumentNullValueException(nameof(number));
        var application = new VisaApplication(Guid.NewGuid(), number, this);
        _applications.Add(application);
        return application;
    }

    public void DeleteApplication(VisaApplication application)
    {
        if (application == null) throw new ArgumentNullValueException(nameof(application));
        if (application.Applicant != this)
            throw new ApplicationNotBelongApplicantException(application, this);
        _applications.Remove(application);
    }

    public override string ToString()
        => $"{LastName} {FirstName} {MiddleName}";
}
