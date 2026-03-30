using Domain.ValueObject.Base;
using Domain.ValueObject.Validators;

namespace Domain.ValueObject;

public class ApplicationStatus : ValueObject<ApplicationStatus>
{
    public bool FormFilled { get; }
    public bool DocumentsChecked { get; }
    public bool ResultChecked { get; }

    public ApplicationStatus(bool formFilled, bool documentsChecked, bool resultChecked)
        : base(new ApplicationStatusValidator(), new ApplicationStatusValue(formFilled, documentsChecked, resultChecked))
    {
        FormFilled = formFilled;
        DocumentsChecked = documentsChecked;
        ResultChecked = resultChecked;
    }

    public ApplicationStatus FillForm() =>
        FormFilled ? this : new ApplicationStatus(true, DocumentsChecked, ResultChecked);

    public ApplicationStatus CheckDocuments() =>
        !FormFilled
            ? throw new InvalidOperationException("Cannot check documents before form is filled")
            : new ApplicationStatus(FormFilled, true, ResultChecked);

    public ApplicationStatus CheckResult() =>
        !DocumentsChecked
            ? throw new InvalidOperationException("Cannot check result before documents are checked")
            : new ApplicationStatus(FormFilled, DocumentsChecked, true);

    private record ApplicationStatusValue(bool FormFilled, bool DocumentsChecked, bool ResultChecked);
}