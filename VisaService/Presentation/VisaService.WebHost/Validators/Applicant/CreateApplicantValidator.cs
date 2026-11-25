using FluentValidation;
using VisaService.WebHost.Requests.Applicant;
using VisaService.WebHost.Validators.Base;

namespace VisaService.WebHost.Validators.Applicant;

public class CreateApplicantValidator : AbstractValidator<CreateApplicantRequest>
{
    public CreateApplicantValidator()
    {
        RuleFor(applicant => applicant.LastName)
            .SetValidator(new PersonNamePresentationValidator());

        RuleFor(applicant => applicant.FirstName)
            .SetValidator(new PersonNamePresentationValidator());

        RuleFor(applicant => applicant.MiddleName)
            .SetValidator(new PersonNamePresentationValidator());

        RuleFor(applicant => applicant.Email)
            .SetValidator(new EmailPresentationValidator());

        RuleFor(applicant => applicant.Phone)
            .SetValidator(new PhonePresentationValidator());
    }
}
