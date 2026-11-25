using VisaService.Application.Models.Base;

namespace VisaService.Application.Models.VisaApplication;

public record class VisaApplicationModel(
    Guid Id,
    string Number,
    Guid ApplicantId,
    Guid? AssignedOfficerId,
    bool FormFilled,
    bool DocumentsChecked,
    bool ResultChecked,
    bool? IsVisaIssued) : IModel<Guid>;
