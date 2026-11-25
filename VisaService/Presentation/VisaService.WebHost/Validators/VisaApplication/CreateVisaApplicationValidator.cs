using FluentValidation;
using VisaService.WebHost.Requests.VisaApplication;
using VisaService.WebHost.Validators.Base;

namespace VisaService.WebHost.Validators.VisaApplication;

public class CreateVisaApplicationValidator : AbstractValidator<CreateVisaApplicationRequest>
{
    public CreateVisaApplicationValidator()
    {
        RuleFor(application => application.ApplicantId)
            .SetValidator(new GuidPresentationValidator());

        RuleFor(application => application.Number)
            .SetValidator(new ApplicationNumberPresentationValidator());
    }
}
