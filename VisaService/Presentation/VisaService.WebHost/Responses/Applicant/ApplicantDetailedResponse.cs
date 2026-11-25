using VisaService.WebHost.Responses.VisaApplication;

namespace VisaService.WebHost.Responses.Applicant;

public record class ApplicantDetailedResponse(
    Guid Id,
    string LastName,
    string FirstName,
    string MiddleName,
    string Email,
    string Phone,
    IEnumerable<VisaApplicationShortResponse> Applications);
