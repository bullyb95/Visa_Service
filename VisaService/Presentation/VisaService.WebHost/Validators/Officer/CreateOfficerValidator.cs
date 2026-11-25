using FluentValidation;
using VisaService.WebHost.Requests.Officer;
using VisaService.WebHost.Validators.Base;

namespace VisaService.WebHost.Validators.Officer;

public class CreateOfficerValidator : AbstractValidator<CreateOfficerRequest>
{
    public CreateOfficerValidator()
    {
        RuleFor(officer => officer.LastName)
            .SetValidator(new PersonNamePresentationValidator());

        RuleFor(officer => officer.FirstName)
            .SetValidator(new PersonNamePresentationValidator());

        RuleFor(officer => officer.MiddleName)
            .SetValidator(new PersonNamePresentationValidator());

        RuleFor(officer => officer.Position)
            .SetValidator(new OfficerPositionPresentationValidator());
    }
}
