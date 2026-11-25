using FluentValidation;
using VisaService.ValueObjects.Validators;

namespace VisaService.WebHost.Validators.Base;

public class ApplicationNumberPresentationValidator : AbstractValidator<string>
{
    public ApplicationNumberPresentationValidator()
    {
        RuleFor(number => number)
            .NotNull()
            .NotEmpty()
            .MinimumLength(ApplicationNumberValidator.MIN_LENGTH)
            .MaximumLength(ApplicationNumberValidator.MAX_LENGTH);
    }
}
