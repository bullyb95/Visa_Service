using VisaService.Domain.Entities;

namespace VisaService.Domain.Repositories.Abstractions;

public interface IOfficerRepository : IRepository<Officer, Guid>
{
    Task<IEnumerable<Officer>> GetByLastNameAsync(string lastName, CancellationToken cancellationToken);
}
