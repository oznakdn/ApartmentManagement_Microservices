using FluentValidation;

namespace Account.Application.Commands.Login;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Email is required!");
        RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password is required!");
    }
}
