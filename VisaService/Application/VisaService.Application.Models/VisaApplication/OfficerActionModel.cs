namespace VisaService.Application.Models.VisaApplication;

/// <summary>Действие визового офицера над заявкой (принять, проверить, выдать, отказать).</summary>
public record class OfficerActionModel(Guid OfficerId, Guid ApplicationId);
