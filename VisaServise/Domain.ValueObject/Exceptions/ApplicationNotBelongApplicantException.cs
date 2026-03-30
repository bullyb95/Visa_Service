using Domain.Visa_Servise;

namespace Domain.ValueObject.Exceptions;

public class ApplicationNotBelongApplicantException(VisaApplication application, Applicant applicant)
    : InvalidOperationException($"Application {application.Id} does not belong to applicant {applicant.Id}")
{
    public VisaApplication Application => application;
    public Applicant Applicant => applicant;
}