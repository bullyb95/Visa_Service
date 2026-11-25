namespace VisaService.WebHost.Responses.VisaApplication;

public record class VisaApplicationShortResponse(Guid Id, string Number, Guid ApplicantId);
