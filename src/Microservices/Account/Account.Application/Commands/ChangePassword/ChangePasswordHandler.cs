using Account.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Account.Application.Commands.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, ChangePasswordResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IValidator<ChangePasswordRequest> _validator;

    public ChangePasswordHandler(UserManager<User> userManager,IValidator<ChangePasswordRequest> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<ChangePasswordResponse> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {

        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
            return new ChangePasswordResponse(false, Errors: validationResult.Errors.Select(x => x.ErrorMessage).ToArray());


        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return new ChangePasswordResponse(false, "User not found!");


        var checkPassword = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
        if (!checkPassword)
            return new ChangePasswordResponse(false, "Wrong password!");


        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
            return new ChangePasswordResponse(false, "Change password failed!");

        return new ChangePasswordResponse(true, "Change password successfully!");
    }
}
