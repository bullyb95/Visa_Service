using FluentValidation;
using VisaService.WebHost.Requests.VisaApplication;
using VisaService.WebHost.Validators.Base;

namespace VisaService.WebHost.Validators.VisaApplication;

public class OfficerActionValidator : AbstractValidator<OfficerActionRequest>
{
    public OfficerActionValidator()
    {
        RuleFor(action => action.OfficerId)
            .SetValidator(new GuidPresentationValidator());

        RuleFor(action => action.ApplicationId)
            .SetValidator(new GuidPresentationValidator());
    }
}
