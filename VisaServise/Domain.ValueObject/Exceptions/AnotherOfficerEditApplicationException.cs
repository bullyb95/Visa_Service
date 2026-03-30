using Domain.Visa_Servise;

namespace Domain.ValueObject.Exceptions;

public class AnotherOfficerEditApplicationException(VisaApplication application, Officer officer)
    : InvalidOperationException($"Officer {officer.Id} cannot edit application {application.Id} owned by officer {application.Officer?.Id}")
{
    public VisaApplication Application => application;
    public Officer Officer => officer;
}