namespace VisaService.WebHost.Responses.Applicant;

public record class ApplicantShortResponse(Guid Id, string LastName, string FirstName, string MiddleName);
