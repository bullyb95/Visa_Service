using VisaService.Domain.Entities;

namespace VisaService.Domain.Exceptions;

public class AnotherOfficerEditApplicationException(VisaApplication application, Officer officer)
    : InvalidOperationException(
        $"Офицер {officer} не может изменить заявку {application.Number}, назначенную другому офицеру")
{
    public VisaApplication Application => application;
    public Officer Officer => officer;
}
