using Microsoft.EntityFrameworkCore;
using VisaService.Domain.Entities;
using VisaService.Domain.Repositories.Abstractions;
using VisaService.Infrastructure.EntityFramework;
using VisaService.ValueObjects;

namespace VisaService.Infrastructure.Repositories.Implementations.EF;

public class EfOfficerRepository(ApplicationDbContext context)
    : EfRepository<Officer, Guid>(context), IOfficerRepository
{
    private readonly DbSet<Officer> _officers = context.Set<Officer>();

    public async Task<IEnumerable<Officer>> GetByLastNameAsync(string lastName, CancellationToken cancellationToken)
        => await _officers
            .AsNoTracking()
            .Where(o => o.LastName.Equals(new LastName(lastName)))
            .ToListAsync(cancellationToken);
}
