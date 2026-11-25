using FluentValidation;
using VisaService.ValueObjects.Validators;

namespace VisaService.WebHost.Validators.Base;

public class OfficerPositionPresentationValidator : AbstractValidator<string>
{
    public OfficerPositionPresentationValidator()
    {
        RuleFor(position => position)
            .NotNull()
            .NotEmpty()
            .MinimumLength(OfficerPositionValidator.MIN_LENGTH)
            .MaximumLength(OfficerPositionValidator.MAX_LENGTH);
    }
}
