using Domain.ValueObject;
using Domain.ValueObject.Base;
using Domain.ValueObject.Exceptions;

namespace Domain.Visa_Servise;

public class VisaApplication : Entity<Guid>
{
    public Applicant Applicant { get; private set; } = default!;
    public Officer? Officer { get; private set; } = null;
    public ApplicationNumber Number { get; private set; } = default!;
    public ApplicationStatus Status { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    protected VisaApplication() : base(Guid.NewGuid()) { }

    public VisaApplication(Guid id, Applicant applicant, ApplicationNumber number, Officer? officer = null)
        : base(id)
    {
        Applicant = applicant ?? throw new ArgumentNullValueException(nameof(applicant));
        Number = number ?? throw new ArgumentNullValueException(nameof(number));
        Officer = officer;
        Status = new ApplicationStatus("Draft");
        CreatedAt = DateTime.UtcNow;
    }

    internal void SetOfficer(Officer officer)
    {
        Officer = officer ?? throw new ArgumentNullValueException(nameof(officer));
        UpdatedAt = DateTime.UtcNow;
    }

    internal void SetStatus(ApplicationStatus status)
    {
        Status = status ?? throw new ArgumentNullValueException(nameof(status));
        UpdatedAt = DateTime.UtcNow;
    }

    public void FillForm()
    {
        if (Status.Value != "Draft")
            throw new InvalidApplicationStateException(Id.ToString(), "fill form");
        Status = new ApplicationStatus("Submitted");
        UpdatedAt = DateTime.UtcNow;
    }

    public void CheckDocuments()
    {
        if (Status.Value != "Submitted")
            throw new InvalidApplicationStateException(Id.ToString(), "check documents");
        Status = new ApplicationStatus("UnderReview");
        UpdatedAt = DateTime.UtcNow;
    }

    public void ApproveVisa(Officer officer)
    {
        if (Status.Value != "UnderReview")
            throw new InvalidApplicationStateException(Id.ToString(), "approve visa");
        Officer = officer ?? throw new ArgumentNullValueException(nameof(officer));
        Status = new ApplicationStatus("Approved");
        UpdatedAt = DateTime.UtcNow;
    }

    public void RejectVisa(Officer officer)
    {
        if (Status.Value != "UnderReview")
            throw new InvalidApplicationStateException(Id.ToString(), "reject visa");
        Officer = officer ?? throw new ArgumentNullValueException(nameof(officer));
        Status = new ApplicationStatus("Rejected");
        UpdatedAt = DateTime.UtcNow;
    }
}
