using FluentValidation;
using VisaService.ValueObjects.Validators;

namespace VisaService.WebHost.Validators.Base;

public class PhonePresentationValidator : AbstractValidator<string>
{
    public PhonePresentationValidator()
    {
        RuleFor(phone => phone)
            .NotNull()
            .NotEmpty()
            .MinimumLength(PhoneValidator.MIN_LENGTH)
            .MaximumLength(PhoneValidator.MAX_LENGTH)
            .Matches(@"^\+?[0-9\s\-()]+$")
            .WithMessage("Телефон может содержать только цифры, пробелы, '+', '-', '(' и ')'");
    }
}
