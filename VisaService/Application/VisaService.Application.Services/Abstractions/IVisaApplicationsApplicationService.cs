using VisaService.Application.Models.VisaApplication;

namespace VisaService.Application.Services.Abstractions;

public interface IVisaApplicationsApplicationService
{
    Task<VisaApplicationModel?> GetVisaApplicationByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<VisaApplicationModel>> GetVisaApplicationsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<VisaApplicationModel>> GetVisaApplicationsByApplicantIdAsync(Guid applicantId, CancellationToken cancellationToken);

    Task<IEnumerable<VisaApplicationModel>> GetVisaApplicationsByOfficerIdAsync(Guid officerId, CancellationToken cancellationToken);

    Task<bool> IsNumberTakenAsync(string number, CancellationToken cancellationToken);
}
