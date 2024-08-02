using FluentValidation;

namespace Account.Application.Commands.ChangeEmail;

public class ChangeEmailValidator : AbstractValidator<ChangeEmailRequest>
{
    public ChangeEmailValidator()
    {
        RuleFor(x => x.CurrentEmail).NotEmpty().NotNull().EmailAddress();
        RuleFor(x => x.NewEmail).NotEmpty().NotNull().EmailAddress();
    }
}
