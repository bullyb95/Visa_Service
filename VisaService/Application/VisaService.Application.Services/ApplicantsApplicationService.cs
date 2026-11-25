using AutoMapper;
using VisaService.Application.Models.Applicant;
using VisaService.Application.Services.Abstractions;
using VisaService.Domain.Entities;
using VisaService.Domain.Repositories.Abstractions;
using VisaService.ValueObjects;

namespace VisaService.Application.Services;

public class ApplicantsApplicationService(IApplicantRepository repository, IMapper mapper) : IApplicantsApplicationService
{
    public async Task<IEnumerable<ApplicantModel>> GetApplicantsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
            .Select(mapper.Map<ApplicantModel>);

    public async Task<ApplicantModel?> GetApplicantByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var applicant = await repository.GetByIdAsync(id, cancellationToken);
        return applicant is null ? null : mapper.Map<ApplicantModel>(applicant);
    }

    public async Task<IEnumerable<ApplicantModel>> GetApplicantsByLastNameAsync(string lastName, CancellationToken cancellationToken = default)
        => (await repository.GetByLastNameAsync(lastName, cancellationToken))
            .Select(mapper.Map<ApplicantModel>);

    public async Task<ApplicantModel?> CreateApplicantAsync(CreateApplicantModel applicantInformation, CancellationToken cancellationToken = default)
    {
        Applicant applicant = new(
            new LastName(applicantInformation.LastName),
            new FirstName(applicantInformation.FirstName),
            new MiddleName(applicantInformation.MiddleName),
            new ContactInfo(applicantInformation.Email, applicantInformation.Phone));

        var createdApplicant = await repository.AddAsync(applicant, cancellationToken);
        return createdApplicant is null ? null : mapper.Map<ApplicantModel>(createdApplicant);
    }

    public async Task<bool> DeleteApplicantAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var applicant = await repository.GetByIdAsync(id, cancellationToken);
        return applicant is null ? false : await repository.DeleteAsync(applicant, cancellationToken);
    }
}
