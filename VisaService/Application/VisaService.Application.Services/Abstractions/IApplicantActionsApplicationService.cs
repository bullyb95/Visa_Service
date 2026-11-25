using VisaService.Application.Models.VisaApplication;

namespace VisaService.Application.Services.Abstractions;

/// <summary>Use cases заявителя. null в ответе — заявитель или заявка не найдены.</summary>
public interface IApplicantActionsApplicationService
{
    Task<VisaApplicationModel?> CreateApplicationAsync(CreateVisaApplicationModel applicationInformation, CancellationToken cancellationToken);

    Task<VisaApplicationModel?> FillFormAsync(ApplicantActionModel action, CancellationToken cancellationToken);

    Task<VisaApplicationModel?> SubmitDocumentsAsync(ApplicantActionModel action, CancellationToken cancellationToken);

    Task<VisaApplicationResultModel?> ReceiveResultAsync(ApplicantActionModel action, CancellationToken cancellationToken);

    Task<bool> DeleteApplicationAsync(ApplicantActionModel action, CancellationToken cancellationToken);
}
