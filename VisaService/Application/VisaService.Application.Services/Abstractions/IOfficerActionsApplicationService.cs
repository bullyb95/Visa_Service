using VisaService.Application.Models.VisaApplication;

namespace VisaService.Application.Services.Abstractions;

/// <summary>Use cases визового офицера. null в ответе — офицер или заявка не найдены.</summary>
public interface IOfficerActionsApplicationService
{
    Task<VisaApplicationModel?> AcceptApplicationAsync(OfficerActionModel action, CancellationToken cancellationToken);

    Task<VisaApplicationModel?> VerifyDataAsync(OfficerActionModel action, CancellationToken cancellationToken);

    Task<VisaApplicationModel?> IssueVisaAsync(OfficerActionModel action, CancellationToken cancellationToken);

    Task<VisaApplicationModel?> RejectVisaAsync(OfficerActionModel action, CancellationToken cancellationToken);
}
