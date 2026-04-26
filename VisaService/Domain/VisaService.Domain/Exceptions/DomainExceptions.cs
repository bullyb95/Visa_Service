namespace VisaService.Domain.Exceptions;

public class ArgumentNullValueException(string paramName)
    : ArgumentNullException(paramName, $"Аргумент \"{paramName}\" не может быть null");

public class AnotherOfficerEditApplicationException(VisaApplication application, Officer officer)
    : InvalidOperationException(
        $"Офицер {officer} не может изменить заявку {application.Number}, назначенную другому офицеру")
{
    public VisaApplication Application => application;
    public Officer Officer => officer;
}

public class ApplicationNotBelongApplicantException(VisaApplication application, Applicant applicant)
    : InvalidOperationException(
        $"Заявка {application.Number} не принадлежит заявителю {applicant} (id = {applicant.Id})")
{
    public VisaApplication Application => application;
    public Applicant Applicant => applicant;
}

public class InvalidApplicationStateException(VisaApplication application, string attemptedAction)
    : InvalidOperationException(
        $"Невозможно выполнить '{attemptedAction}' для заявки {application.Number} в текущем состоянии")
{
    public VisaApplication Application => application;
    public string AttemptedAction => attemptedAction;
}
