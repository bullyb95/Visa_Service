using VisaService.Domain.Entities;

namespace VisaService.Domain.Exceptions;

public class InvalidApplicationStateException(VisaApplication application, string attemptedAction)
    : InvalidOperationException(
        $"Невозможно выполнить '{attemptedAction}' для заявки {application.Number} в текущем состоянии")
{
    public VisaApplication Application => application;
    public string AttemptedAction => attemptedAction;
}
