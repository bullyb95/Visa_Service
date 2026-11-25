namespace VisaService.WebHost.Responses.VisaApplication;

public record class VisaApplicationDetailedResponse(
    Guid Id,
    string Number,
    Guid ApplicantId,
    Guid? AssignedOfficerId,
    bool FormFilled,
    bool DocumentsChecked,
    bool ResultChecked,
    bool? IsVisaIssued);
