namespace VisaService.WebHost.Responses.Officer;

public record class OfficerDetailedResponse(
    Guid Id,
    string LastName,
    string FirstName,
    string MiddleName,
    string Position);
