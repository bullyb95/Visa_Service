using AutoMapper;
using VisaService.Application.Models.VisaApplication;
using VisaService.Application.Services.Abstractions;
using VisaService.Domain.Entities;
using VisaService.Domain.Repositories.Abstractions;

namespace VisaService.Application.Services;

/// <summary>Сценарии визового офицера. Правила переходов состояния заявки — в Domain.</summary>
public class OfficerActionsApplicationService(
    IOfficerRepository officerRepository,
    IVisaApplicationRepository applicationRepository,
    IMapper mapper)
    : IOfficerActionsApplicationService
{
    public Task<VisaApplicationModel?> AcceptApplicationAsync(OfficerActionModel action, CancellationToken cancellationToken = default)
        => ExecuteAsync(action, (officer, application) => officer.AcceptApplication(application), cancellationToken);

    public Task<VisaApplicationModel?> VerifyDataAsync(OfficerActionModel action, CancellationToken cancellationToken = default)
        => ExecuteAsync(action, (officer, application) => officer.VerifyData(application), cancellationToken);

    public Task<VisaApplicationModel?> IssueVisaAsync(OfficerActionModel action, CancellationToken cancellationToken = default)
        => ExecuteAsync(action, (officer, application) => officer.IssueVisa(application), cancellationToken);

    public Task<VisaApplicationModel?> RejectVisaAsync(OfficerActionModel action, CancellationToken cancellationToken = default)
        => ExecuteAsync(action, (officer, application) => officer.RejectVisa(application), cancellationToken);

    private async Task<VisaApplicationModel?> ExecuteAsync(
        OfficerActionModel action,
        Action<Officer, VisaApplication> domainAction,
        CancellationToken cancellationToken)
    {
        var officer = await officerRepository.GetByIdAsync(action.OfficerId, cancellationToken);
        if (officer is null)
            return null;

        var application = await applicationRepository.GetByIdAsync(action.ApplicationId, cancellationToken);
        if (application is null)
            return null;

        domainAction(officer, application);
        await applicationRepository.UpdateAsync(application, cancellationToken);
        return mapper.Map<VisaApplicationModel>(application);
    }
}
