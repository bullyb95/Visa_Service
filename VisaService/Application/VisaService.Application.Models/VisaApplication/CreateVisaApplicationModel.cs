using VisaService.Application.Models.Base;

namespace VisaService.Application.Models.VisaApplication;

public record class CreateVisaApplicationModel(Guid ApplicantId, string Number) : ICreateModel;
