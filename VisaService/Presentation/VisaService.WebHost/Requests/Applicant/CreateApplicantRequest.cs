namespace VisaService.WebHost.Requests.Applicant;

public record class CreateApplicantRequest(
    string LastName,
    string FirstName,
    string MiddleName,
    string Email,
    string Phone);
