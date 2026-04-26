namespace Domain.ValueObject.Exceptions;

public class InvalidApplicationStateException(string applicationId, string attemptedAction)
    : InvalidOperationException($"Cannot {attemptedAction} on application {applicationId} in its current state.")
{
    public string ApplicationId => applicationId;
    public string AttemptedAction => attemptedAction;
}