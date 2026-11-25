namespace VisaService.WebHost.Requests.VisaApplication;

public record class ApplicantActionRequest(Guid ApplicantId, Guid ApplicationId);
