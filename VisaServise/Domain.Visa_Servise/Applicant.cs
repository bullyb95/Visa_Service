using Domain.ValueObject;
using Domain.ValueObject.Base;
using Domain.ValueObject.Exceptions;

namespace Domain.Visa_Servise;

public class Applicant : Entity<Guid>
{
    public PersonName Name { get; private set; }
    public ContactInfo? Contact { get; private set; }

    private readonly List<VisaApplication> _applications = new();
    public IReadOnlyCollection<VisaApplication> Applications => _applications.AsReadOnly();

    protected Applicant() { }

    public Applicant(Guid id, PersonName name, ContactInfo? contact = null) : base(id)
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Contact = contact;
    }

    public VisaApplication CreateApplication(ApplicationNumber number, Officer? assignedOfficer = null)
    {
        var application = new VisaApplication(Guid.NewGuid(), this, number, assignedOfficer);
        _applications.Add(application);
        return application;
    }

    public bool EditApplication(VisaApplication application, Officer? newOfficer = null, ApplicationStatus? newStatus = null)
    {
        if (application.Applicant != this)
            throw new ApplicationNotBelongApplicantException(application, this);
        if (!_applications.Contains(application))
            throw new ApplicationNotBelongApplicantException(application, this);

        bool changed = false;
        if (newOfficer != null && application.Officer != newOfficer)
        {
            application.SetOfficer(newOfficer);
            changed = true;
        }
        if (newStatus != null && application.Status != newStatus)
        {
            application.SetStatus(newStatus);
            changed = true;
        }
        return changed;
    }

    public bool DeleteApplication(VisaApplication application)
    {
        if (application.Applicant != this)
            throw new ApplicationNotBelongApplicantException(application, this);
        if (!_applications.Contains(application))
            throw new ApplicationNotBelongApplicantException(application, this);
        return _applications.Remove(application);
    }
}