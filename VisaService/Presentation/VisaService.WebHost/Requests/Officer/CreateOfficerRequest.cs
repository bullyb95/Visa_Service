namespace VisaService.WebHost.Requests.Officer;

public record class CreateOfficerRequest(
    string LastName,
    string FirstName,
    string MiddleName,
    string Position);
