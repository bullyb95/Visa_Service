using AutoMapper;
using VisaService.Application.Models.VisaApplication;
using VisaService.Application.Services.Abstractions;
using VisaService.Domain.Repositories.Abstractions;

namespace VisaService.Application.Services;

public class VisaApplicationsApplicationService(IVisaApplicationRepository repository, IMapper mapper)
    : IVisaApplicationsApplicationService
{
    public async Task<IEnumerable<VisaApplicationModel>> GetVisaApplicationsAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
            .Select(mapper.Map<VisaApplicationModel>);

    public async Task<VisaApplicationModel?> GetVisaApplicationByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var application = await repository.GetByIdAsync(id, cancellationToken);
        return application is null ? null : mapper.Map<VisaApplicationModel>(application);
    }

    public async Task<IEnumerable<VisaApplicationModel>> GetVisaApplicationsByApplicantIdAsync(Guid applicantId, CancellationToken cancellationToken = default)
        => (await repository.GetByApplicantIdAsync(applicantId, cancellationToken))
            .Select(mapper.Map<VisaApplicationModel>);

    public async Task<IEnumerable<VisaApplicationModel>> GetVisaApplicationsByOfficerIdAsync(Guid officerId, CancellationToken cancellationToken = default)
        => (await repository.GetByOfficerIdAsync(officerId, cancellationToken))
            .Select(mapper.Map<VisaApplicationModel>);

    public async Task<bool> IsNumberTakenAsync(string number, CancellationToken cancellationToken = default)
        => await repository.GetByNumberAsync(number, cancellationToken) is not null;
}
