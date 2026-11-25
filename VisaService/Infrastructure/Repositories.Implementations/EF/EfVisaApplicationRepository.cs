using Microsoft.EntityFrameworkCore;
using VisaService.Domain.Entities;
using VisaService.Domain.Repositories.Abstractions;
using VisaService.Infrastructure.EntityFramework;
using VisaService.ValueObjects;

namespace VisaService.Infrastructure.Repositories.Implementations.EF;

public class EfVisaApplicationRepository(ApplicationDbContext context)
    : EfRepository<VisaApplication, Guid>(context), IVisaApplicationRepository
{
    private readonly DbSet<VisaApplication> _applications = context.Set<VisaApplication>();

    public override async Task<IEnumerable<VisaApplication>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        => await (asNoTracking ? _applications.AsNoTracking() : _applications)
            .Include(a => a.Applicant)
            .Include(a => a.AssignedOfficer)
            .ToListAsync(cancellationToken);

    public override Task<VisaApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _applications
            .Include(a => a.Applicant)
            .Include(a => a.AssignedOfficer)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task<IEnumerable<VisaApplication>> GetByApplicantIdAsync(Guid applicantId, CancellationToken cancellationToken)
        => await _applications
            .AsNoTracking()
            .Include(a => a.Applicant)
            .Include(a => a.AssignedOfficer)
            .Where(a => a.Applicant.Id == applicantId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<VisaApplication>> GetByOfficerIdAsync(Guid officerId, CancellationToken cancellationToken)
        => await _applications
            .AsNoTracking()
            .Include(a => a.Applicant)
            .Include(a => a.AssignedOfficer)
            .Where(a => a.AssignedOfficer != null && a.AssignedOfficer.Id == officerId)
            .ToListAsync(cancellationToken);

    public Task<VisaApplication?> GetByNumberAsync(string number, CancellationToken cancellationToken)
        => _applications
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Number.Equals(new ApplicationNumber(number)), cancellationToken);
}
