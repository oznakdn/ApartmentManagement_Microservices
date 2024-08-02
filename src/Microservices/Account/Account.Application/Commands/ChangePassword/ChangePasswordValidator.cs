using FluentValidation;

namespace Account.Application.Commands.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage("User Id is required");
        RuleFor(x => x.CurrentPassword).NotEmpty().NotNull().WithMessage("Current Password is required");
        RuleFor(x => x.NewPassword).NotEmpty().NotNull().WithMessage("New Password is required");
    }
}
