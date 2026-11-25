namespace VisaService.WebHost.Requests.VisaApplication;

public record class OfficerActionRequest(Guid OfficerId, Guid ApplicationId);
