using AutoMapper;
using VisaService.Application.Models.Officer;
using VisaService.Application.Services.Abstractions;
using VisaService.Domain.Entities;
using VisaService.Domain.Repositories.Abstractions;
using VisaService.ValueObjects;

namespace VisaService.Application.Services;

public class OfficersApplicationService(IOfficerRepository repository, IMapper mapper) : IOfficersApplicationService
{
    public async Task<IEnumerable<OfficerModel>> GetOfficersAsync(CancellationToken cancellationToken = default)
        => (await repository.GetAllAsync(cancellationToken, true))
            .Select(mapper.Map<OfficerModel>);

    public async Task<OfficerModel?> GetOfficerByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var officer = await repository.GetByIdAsync(id, cancellationToken);
        return officer is null ? null : mapper.Map<OfficerModel>(officer);
    }

    public async Task<IEnumerable<OfficerModel>> GetOfficersByLastNameAsync(string lastName, CancellationToken cancellationToken = default)
        => (await repository.GetByLastNameAsync(lastName, cancellationToken))
            .Select(mapper.Map<OfficerModel>);

    public async Task<OfficerModel?> CreateOfficerAsync(CreateOfficerModel officerInformation, CancellationToken cancellationToken = default)
    {
        Officer officer = new(
            new LastName(officerInformation.LastName),
            new FirstName(officerInformation.FirstName),
            new MiddleName(officerInformation.MiddleName),
            new OfficerPosition(officerInformation.Position));

        var createdOfficer = await repository.AddAsync(officer, cancellationToken);
        return createdOfficer is null ? null : mapper.Map<OfficerModel>(createdOfficer);
    }

    public async Task<bool> DeleteOfficerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var officer = await repository.GetByIdAsync(id, cancellationToken);
        return officer is null ? false : await repository.DeleteAsync(officer, cancellationToken);
    }
}
