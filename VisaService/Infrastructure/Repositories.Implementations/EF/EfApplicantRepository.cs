using Microsoft.EntityFrameworkCore;
using VisaService.Domain.Entities;
using VisaService.Domain.Repositories.Abstractions;
using VisaService.Infrastructure.EntityFramework;
using VisaService.ValueObjects;

namespace VisaService.Infrastructure.Repositories.Implementations.EF;

public class EfApplicantRepository(ApplicationDbContext context)
    : EfRepository<Applicant, Guid>(context), IApplicantRepository
{
    private readonly DbSet<Applicant> _applicants = context.Set<Applicant>();

    public override Task<Applicant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _applicants
            .Include("_applications")
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    // Сравниваем Value Object через Equals: EF переводит это в WHERE last_name = @p.
    // Вариант a.LastName.Value == lastName в SQL не транслируется (так же сделано у преподавателя).
    public async Task<IEnumerable<Applicant>> GetByLastNameAsync(string lastName, CancellationToken cancellationToken)
        => await _applicants
            .AsNoTracking()
            .Where(a => a.LastName.Equals(new LastName(lastName)))
            .ToListAsync(cancellationToken);
}
