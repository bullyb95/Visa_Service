using VisaService.ValueObjects;
using VisaService.ValueObjects.Base;
using VisaService.Domain.Exceptions;

namespace VisaService.Domain.Entities;

public class Applicant : Entity<Guid>
{
    public LastName LastName { get; private set; }
    public FirstName FirstName { get; private set; }
    public MiddleName MiddleName { get; private set; }
    public ContactInfo ContactInfo { get; private set; }

    private readonly ICollection<VisaApplication> _applications = new List<VisaApplication>();
    public IReadOnlyCollection<VisaApplication> Applications => _applications.ToList().AsReadOnly();

    protected Applicant() : base() { }

    public Applicant(
        LastName lastName,
        FirstName firstName,
        MiddleName middleName,
        ContactInfo contactInfo)
        : this(Guid.NewGuid(), lastName, firstName, middleName, contactInfo) { }

    protected Applicant(
        Guid id,
        LastName lastName,
        FirstName firstName,
        MiddleName middleName,
        ContactInfo contactInfo)
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
        var application = new VisaApplication(number, this);
        _applications.Add(application);
        return application;
    }

    public void FillForm(VisaApplication application)
    {
        if (application == null) throw new ArgumentNullValueException(nameof(application));
        if (application.Applicant != this)
            throw new ApplicationNotBelongApplicantException(application, this);
        application.FillForm();
    }

    public void SubmitDocuments(VisaApplication application)
    {
        if (application == null) throw new ArgumentNullValueException(nameof(application));
        if (application.Applicant != this)
            throw new ApplicationNotBelongApplicantException(application, this);
        application.SubmitDocuments();
    }

    public string ReceiveResult(VisaApplication application)
    {
        if (application == null) throw new ArgumentNullValueException(nameof(application));
        if (application.Applicant != this)
            throw new ApplicationNotBelongApplicantException(application, this);
        return application.GetResult();
    }

    public void DeleteApplication(VisaApplication application)
    {
        if (application == null) throw new ArgumentNullValueException(nameof(application));
        if (application.Applicant != this)
            throw new ApplicationNotBelongApplicantException(application, this);
        if (!_applications.Contains(application))
            throw new ApplicationNotBelongApplicantException(application, this);
        _applications.Remove(application);
    }

    public override string ToString()
        => $"{LastName} {FirstName} {MiddleName}";
}