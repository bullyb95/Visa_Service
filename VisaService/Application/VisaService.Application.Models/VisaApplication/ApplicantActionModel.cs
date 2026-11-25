namespace VisaService.Application.Models.VisaApplication;

/// <summary>Действие заявителя над своей заявкой (заполнить анкету, подать документы, удалить).</summary>
public record class ApplicantActionModel(Guid ApplicantId, Guid ApplicationId);
