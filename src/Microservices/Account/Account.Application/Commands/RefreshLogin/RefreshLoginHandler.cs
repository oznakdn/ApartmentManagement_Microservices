using Account.Application.Commands.Login;
using Account.Application.Events.Logined;
using Account.Application.Events.RefreshLogined;
using Account.Application.Helpers.Token;
using Account.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.RefreshLogin;

public class RefreshLoginHandler : IRequestHandler<RefreshLoginRequest, IResult<LoginResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMediator _eventHandler;
    private readonly JwtTokenHelper _tokenHelper;

    public RefreshLoginHandler(UserManager<User> userManager, IMediator eventHandler, JwtTokenHelper tokenHelper)
    {
        _userManager = userManager;
        _eventHandler = eventHandler;
        _tokenHelper = tokenHelper;
    }

    public async Task<IResult<LoginResponse>> Handle(RefreshLoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.RefreshToken == request.RefreshToken);

        if (user is null)
            return Result<LoginResponse>.Failure(message: "Invalid Credentials!");

        var roles = await _userManager.GetRolesAsync(user);

        var token = await _tokenHelper.GenerateToken(user);

        user.SetRefreshToken(token.RefreshToken, token.RefreshExpIn);
        await _userManager.UpdateAsync(user);


        var value = new LoginResponse(
            user.Id,
            user.Email!,
            roles.SingleOrDefault(),
            token.AccessToken,
            token.AccessExpIn,
            token.RefreshToken,
            token.RefreshExpIn,
            user.SiteId,
            user.UnitId);

        await _eventHandler.Publish(new RefreshLoginedEvent(request.RefreshToken, token.RefreshToken, token.RefreshExpIn));

        return Result<LoginResponse>.Success(value);
    }
}
