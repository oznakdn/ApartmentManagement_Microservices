using Account.Application.Events.Logined;
using Account.Application.Helpers.Token;
using Account.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.Login;

public class LoginHandler : IRequestHandler<LoginRequest, IResult<LoginResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMediator _eventHandler;
    private readonly JwtTokenHelper _tokenHelper;
    private readonly IValidator<LoginRequest> _validator;
    public LoginHandler(UserManager<User> userManager, JwtTokenHelper tokenHelper, IValidator<LoginRequest> validator, IMediator eventHandler)
    {
        _userManager = userManager;
        _tokenHelper = tokenHelper;
        _validator = validator;
        _eventHandler = eventHandler;
    }

    public async Task<IResult<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result<LoginResponse>.Failure(errors: validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result<LoginResponse>.Failure(message: "Invalid Credentials!");

        var result = await _userManager
           .CheckPasswordAsync(user, request.Password);

        if (!result)
            return Result<LoginResponse>.Failure(message: "Invalid Credentials!");

        var roles = await _userManager.GetRolesAsync(user);

        var token = await _tokenHelper.GenerateToken(user);

        user.SetRefreshToken(token.RefreshToken, token.RefreshExpIn);
        await _userManager.UpdateAsync(user);


        await _eventHandler.Publish(new LoginedEvent(user.Id, token.RefreshToken, token.RefreshExpIn));

        var value = new LoginResponse(
            user.Id,
            user.Email!,
            roles.SingleOrDefault(),
            token.AccessToken,
            token.AccessExpIn,
            token.RefreshToken,
            token.RefreshExpIn,
            user.SiteId,
            user.UnitId
            );

        return Result<LoginResponse>.Success(value);

    }
}
