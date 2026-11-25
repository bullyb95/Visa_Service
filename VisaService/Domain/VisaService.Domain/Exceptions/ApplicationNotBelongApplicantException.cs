using VisaService.Domain.Entities;

namespace VisaService.Domain.Exceptions;

public class ApplicationNotBelongApplicantException(VisaApplication application, Applicant applicant)
    : InvalidOperationException(
        $"Заявка {application.Number} не принадлежит заявителю {applicant} (id = {applicant.Id})")
{
    public VisaApplication Application => application;
    public Applicant Applicant => applicant;
}
