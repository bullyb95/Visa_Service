using VisaService.Application.Models.Base;
using VisaService.Application.Models.VisaApplication;

namespace VisaService.Application.Models.Applicant;

public record class ApplicantModel(
    Guid Id,
    string LastName,
    string FirstName,
    string MiddleName,
    string Email,
    string Phone) : PersonModel(Id, LastName, FirstName, MiddleName)
{
    public IEnumerable<VisaApplicationModel> Applications { get; init; } = [];
}
