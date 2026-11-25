using FluentValidation;
using VisaService.ValueObjects.Validators;

namespace VisaService.WebHost.Validators.Base;

public class PersonNamePresentationValidator : AbstractValidator<string>
{
    public PersonNamePresentationValidator()
    {
        RuleFor(name => name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(PersonNameValidator.MIN_LENGTH)
            .MaximumLength(PersonNameValidator.MAX_LENGTH);
    }
}
