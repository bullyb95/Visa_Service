using FluentValidation;
using VisaService.ValueObjects.Validators;

namespace VisaService.WebHost.Validators.Base;

public class EmailPresentationValidator : AbstractValidator<string>
{
    public EmailPresentationValidator()
    {
        RuleFor(email => email)
            .NotNull()
            .NotEmpty()
            .MaximumLength(EmailValidator.MAX_LENGTH)
            .EmailAddress();
    }
}
