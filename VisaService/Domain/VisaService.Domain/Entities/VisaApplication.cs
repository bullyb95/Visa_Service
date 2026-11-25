using VisaService.ValueObjects;
using VisaService.ValueObjects.Base;
using VisaService.Domain.Exceptions;

namespace VisaService.Domain.Entities;

public class VisaApplication : Entity<Guid>
{
    public ApplicationNumber Number { get; private set; }
    public Applicant Applicant { get; private set; }
    public Officer? AssignedOfficer { get; private set; }

    public bool FormFilled { get; private set; }
    public bool DocumentsChecked { get; private set; }
    public bool ResultChecked { get; private set; }
    public bool? IsVisaIssued { get; private set; }

    protected VisaApplication() : base() { }

    public VisaApplication(
        ApplicationNumber number,
        Applicant applicant)
        : this(Guid.NewGuid(), number, applicant) { }

    protected VisaApplication(
        Guid id,
        ApplicationNumber number,
        Applicant applicant)
        : base(id)
    {
        Number = number ?? throw new ArgumentNullValueException(nameof(number));
        Applicant = applicant ?? throw new ArgumentNullValueException(nameof(applicant));
    }

    public void FillForm()
    {
        if (FormFilled)
            throw new InvalidApplicationStateException(this, nameof(FillForm));
        FormFilled = true;
    }

    public void SubmitDocuments()
    {
        if (!FormFilled)
            throw new InvalidApplicationStateException(this, nameof(SubmitDocuments));
    }

    public void AcceptApplication(Officer officer)
    {
        if (officer == null) throw new ArgumentNullValueException(nameof(officer));
        if (!FormFilled)
            throw new InvalidApplicationStateException(this, nameof(AcceptApplication));
        AssignedOfficer = officer;
    }

    public void VerifyData(Officer officer)
    {
        if (officer == null) throw new ArgumentNullValueException(nameof(officer));
        if (AssignedOfficer != officer)
            throw new AnotherOfficerEditApplicationException(this, officer);
        DocumentsChecked = true;
    }

    public void IssueVisa(Officer officer)
    {
        if (officer == null) throw new ArgumentNullValueException(nameof(officer));
        if (!DocumentsChecked)
            throw new InvalidApplicationStateException(this, nameof(IssueVisa));
        if (ResultChecked)
            throw new InvalidApplicationStateException(this, nameof(IssueVisa));
        if (AssignedOfficer != officer)
            throw new AnotherOfficerEditApplicationException(this, officer);
        ResultChecked = true;
        IsVisaIssued = true;
    }

    public void RejectVisa(Officer officer)
    {
        if (officer == null) throw new ArgumentNullValueException(nameof(officer));
        if (!DocumentsChecked)
            throw new InvalidApplicationStateException(this, nameof(RejectVisa));
        if (ResultChecked)
            throw new InvalidApplicationStateException(this, nameof(RejectVisa));
        if (AssignedOfficer != officer)
            throw new AnotherOfficerEditApplicationException(this, officer);
        ResultChecked = true;
        IsVisaIssued = false;
    }

    public string GetResult()
    {
        if (!ResultChecked)
            return $"Заявка {Number}: ещё рассматривается";
        return IsVisaIssued == true
            ? $"Заявка {Number}: виза выдана"
            : $"Заявка {Number}: в выдаче визы отказано";
    }
}
