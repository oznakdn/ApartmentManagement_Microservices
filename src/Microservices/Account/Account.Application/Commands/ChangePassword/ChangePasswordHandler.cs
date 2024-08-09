using Account.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, IResult>
{
    private readonly UserManager<User> _userManager;
    private readonly IValidator<ChangePasswordRequest> _validator;

    public ChangePasswordHandler(UserManager<User> userManager, IValidator<ChangePasswordRequest> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<IResult> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {

        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
            return Result.Failure(errors: validationResult.Errors.Select(x => x.ErrorMessage).ToArray());


        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return Result.Failure(message: "User not found!");


        var checkPassword = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
        if (!checkPassword)
            return Result.Failure(message: "Wrong password!");


        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
            return Result.Failure(message: "Change password failed!");

        return Result.Success(message:"Change password successfully!");
    }
}
