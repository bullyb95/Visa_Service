using Domain.Visa_Servise;

namespace Domain.ValueObject.Exceptions;

public class InvalidApplicationStateException(VisaApplication application, string attemptedAction)
    : InvalidOperationException($"Cannot {attemptedAction} on application {application.Id} in its current state.")
{
    public VisaApplication Application => application;
    public string AttemptedAction => attemptedAction;
}