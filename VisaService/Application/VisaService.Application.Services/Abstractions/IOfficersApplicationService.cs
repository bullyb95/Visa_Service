using VisaService.Application.Models.Officer;

namespace VisaService.Application.Services.Abstractions;

public interface IOfficersApplicationService
{
    Task<OfficerModel?> GetOfficerByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<OfficerModel>> GetOfficersByLastNameAsync(string lastName, CancellationToken cancellationToken);

    Task<IEnumerable<OfficerModel>> GetOfficersAsync(CancellationToken cancellationToken);

    Task<OfficerModel?> CreateOfficerAsync(CreateOfficerModel officerInformation, CancellationToken cancellationToken);

    Task<bool> DeleteOfficerAsync(Guid id, CancellationToken cancellationToken);
}
