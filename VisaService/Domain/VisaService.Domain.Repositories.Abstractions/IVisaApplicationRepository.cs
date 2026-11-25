using VisaService.Domain.Entities;

namespace VisaService.Domain.Repositories.Abstractions;

public interface IVisaApplicationRepository : IRepository<VisaApplication, Guid>
{
    Task<IEnumerable<VisaApplication>> GetByApplicantIdAsync(Guid applicantId, CancellationToken cancellationToken);
    Task<IEnumerable<VisaApplication>> GetByOfficerIdAsync(Guid officerId, CancellationToken cancellationToken);
    Task<VisaApplication?> GetByNumberAsync(string number, CancellationToken cancellationToken);
}
