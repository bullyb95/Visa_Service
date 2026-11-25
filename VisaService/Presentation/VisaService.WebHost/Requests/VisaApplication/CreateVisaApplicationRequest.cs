namespace VisaService.WebHost.Requests.VisaApplication;

public record class CreateVisaApplicationRequest(Guid ApplicantId, string Number);
