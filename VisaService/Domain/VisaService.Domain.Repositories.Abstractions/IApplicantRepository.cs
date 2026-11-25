using VisaService.Domain.Entities;

namespace VisaService.Domain.Repositories.Abstractions;

public interface IApplicantRepository : IRepository<Applicant, Guid>
{
    Task<IEnumerable<Applicant>> GetByLastNameAsync(string lastName, CancellationToken cancellationToken);
}
