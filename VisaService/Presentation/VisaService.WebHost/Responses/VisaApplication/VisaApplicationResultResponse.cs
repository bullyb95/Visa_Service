namespace VisaService.WebHost.Responses.VisaApplication;

public record class VisaApplicationResultResponse(Guid ApplicationId, string Number, string Result);
