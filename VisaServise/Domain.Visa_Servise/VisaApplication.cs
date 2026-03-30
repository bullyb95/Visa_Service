using Domain.ValueObject;
using Domain.ValueObject.Base;
using Domain.ValueObject.Exceptions;

namespace Domain.Visa_Servise;

public class VisaApplication : Entity<Guid>
{
    public ApplicationNumber Number { get; private set; }
    public Applicant Applicant { get; private set; }
    public Officer? Officer { get; private set; }
    public ApplicationStatus Status { get; private set; }

    protected VisaApplication() { }

    public VisaApplication(Guid id, Applicant applicant, ApplicationNumber number, Officer? officer = null) : base(id)
    {
        Applicant = applicant ?? throw new ArgumentNullValueException(nameof(applicant));
        Number = number ?? throw new ArgumentNullValueException(nameof(number));
        Officer = officer;
        Status = new ApplicationStatus(false, false, false);
    }

    internal void SetOfficer(Officer officer)
    {
        Officer = officer ?? throw new ArgumentNullValueException(nameof(officer));
    }

    internal void SetStatus(ApplicationStatus newStatus)
    {
        Status = newStatus ?? throw new ArgumentNullValueException(nameof(newStatus));
    }

    public void FillForm() => Status = Status.FillForm();
    public void CheckDocuments() => Status = Status.CheckDocuments();
    public void CheckResult() => Status = Status.CheckResult();
}