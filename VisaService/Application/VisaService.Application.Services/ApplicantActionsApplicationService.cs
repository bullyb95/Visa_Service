using AutoMapper;
using VisaService.Application.Models.VisaApplication;
using VisaService.Application.Services.Abstractions;
using VisaService.Domain.Repositories.Abstractions;
using VisaService.ValueObjects;

namespace VisaService.Application.Services;

/// <summary>
/// Сценарии заявителя. Бизнес-правила проверяет Domain (методы Applicant/VisaApplication),
/// сервис только загружает сущности, вызывает доменный метод и сохраняет результат.
/// Доменные исключения не перехватываются — их превращает в HTTP-ответ ExceptionHandlingMiddleware.
/// </summary>
public class ApplicantActionsApplicationService(
    IApplicantRepository applicantRepository,
    IVisaApplicationRepository applicationRepository,
    IMapper mapper)
    : IApplicantActionsApplicationService
{
    public async Task<VisaApplicationModel?> CreateApplicationAsync(CreateVisaApplicationModel applicationInformation, CancellationToken cancellationToken = default)
    {
        var applicant = await applicantRepository.GetByIdAsync(applicationInformation.ApplicantId, cancellationToken);
        if (applicant is null)
            return null;

        var application = applicant.CreateApplication(new ApplicationNumber(applicationInformation.Number));
        var createdApplication = await applicationRepository.AddAsync(application, cancellationToken);
        return createdApplication is null ? null : mapper.Map<VisaApplicationModel>(createdApplication);
    }

    public async Task<VisaApplicationModel?> FillFormAsync(ApplicantActionModel action, CancellationToken cancellationToken = default)
    {
        var (applicant, application) = await LoadAsync(action, cancellationToken);
        if (applicant is null || application is null)
            return null;

        applicant.FillForm(application);
        await applicationRepository.UpdateAsync(application, cancellationToken);
        return mapper.Map<VisaApplicationModel>(application);
    }

    public async Task<VisaApplicationModel?> SubmitDocumentsAsync(ApplicantActionModel action, CancellationToken cancellationToken = default)
    {
        var (applicant, application) = await LoadAsync(action, cancellationToken);
        if (applicant is null || application is null)
            return null;

        applicant.SubmitDocuments(application);
        await applicationRepository.UpdateAsync(application, cancellationToken);
        return mapper.Map<VisaApplicationModel>(application);
    }

    public async Task<VisaApplicationResultModel?> ReceiveResultAsync(ApplicantActionModel action, CancellationToken cancellationToken = default)
    {
        var (applicant, application) = await LoadAsync(action, cancellationToken);
        if (applicant is null || application is null)
            return null;

        var result = applicant.ReceiveResult(application);
        return new VisaApplicationResultModel(application.Id, application.Number.Value, result);
    }

    public async Task<bool> DeleteApplicationAsync(ApplicantActionModel action, CancellationToken cancellationToken = default)
    {
        var (applicant, application) = await LoadAsync(action, cancellationToken);
        if (applicant is null || application is null)
            return false;

        applicant.DeleteApplication(application);
        return await applicationRepository.DeleteAsync(application, cancellationToken);
    }

    private async Task<(Domain.Entities.Applicant?, Domain.Entities.VisaApplication?)> LoadAsync(
        ApplicantActionModel action, CancellationToken cancellationToken)
    {
        var applicant = await applicantRepository.GetByIdAsync(action.ApplicantId, cancellationToken);
        var application = await applicationRepository.GetByIdAsync(action.ApplicationId, cancellationToken);
        return (applicant, application);
    }
}
