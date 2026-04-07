namespace Domain.ValueObject.Exceptions;

public class ApplicationNotBelongApplicantException(string applicationId, string applicantId)
    : InvalidOperationException($"Application {applicationId} does not belong to applicant {applicantId}")
{
    public string ApplicationId => applicationId;
    public string ApplicantId => applicantId;
}