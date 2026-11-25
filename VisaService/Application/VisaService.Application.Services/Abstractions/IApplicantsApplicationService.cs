using VisaService.Application.Models.Applicant;

namespace VisaService.Application.Services.Abstractions;

public interface IApplicantsApplicationService
{
    Task<ApplicantModel?> GetApplicantByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ApplicantModel>> GetApplicantsByLastNameAsync(string lastName, CancellationToken cancellationToken);

    Task<IEnumerable<ApplicantModel>> GetApplicantsAsync(CancellationToken cancellationToken);

    Task<ApplicantModel?> CreateApplicantAsync(CreateApplicantModel applicantInformation, CancellationToken cancellationToken);

    Task<bool> DeleteApplicantAsync(Guid id, CancellationToken cancellationToken);
}
