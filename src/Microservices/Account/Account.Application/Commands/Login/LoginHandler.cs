using Account.Application.Helpers.Token;
using Account.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Account.Application.Commands.Login;

public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly JwtTokenHelper _tokenHelper;
    private readonly IValidator<LoginRequest> _validator;
    public LoginHandler(UserManager<User> userManager, JwtTokenHelper tokenHelper, IValidator<LoginRequest> validator)
    {
        _userManager = userManager;
        _tokenHelper = tokenHelper;
        _validator = validator;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new LoginResponse(false, Errors: validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new LoginResponse(false, Message: "Invalid Credentials!");

        var result = await _userManager
           .CheckPasswordAsync(user, request.Password);

        if (!result)
            return new LoginResponse(false, Message: "Invalid Credentials!");

        var roles = await _userManager.GetRolesAsync(user);

        var token = await _tokenHelper.GenerateToken(user);

        user.SetRefreshToken(token.RefreshToken, token.RefreshExpIn);
        await _userManager.UpdateAsync(user);

        return new LoginResponse(true, "Login Successful!", Response: new Response(
            user.Id,
            user.Email!,
            roles.SingleOrDefault(),
            token.AccessToken,
            token.AccessExpIn,
            token.RefreshToken,
            token.RefreshExpIn));

    }
}
