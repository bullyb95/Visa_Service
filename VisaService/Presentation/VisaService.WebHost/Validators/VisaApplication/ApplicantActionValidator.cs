using FluentValidation;
using VisaService.WebHost.Requests.VisaApplication;
using VisaService.WebHost.Validators.Base;

namespace VisaService.WebHost.Validators.VisaApplication;

public class ApplicantActionValidator : AbstractValidator<ApplicantActionRequest>
{
    public ApplicantActionValidator()
    {
        RuleFor(action => action.ApplicantId)
            .SetValidator(new GuidPresentationValidator());

        RuleFor(action => action.ApplicationId)
            .SetValidator(new GuidPresentationValidator());
    }
}
