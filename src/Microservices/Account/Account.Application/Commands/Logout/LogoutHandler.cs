using Account.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.Logout;

public class LogoutHandler : IRequestHandler<LogoutRequest, IResult<string>>
{
    private readonly UserManager<User> _userManager;
    public LogoutHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IResult<string>> Handle(LogoutRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.RefreshToken == request.RefreshToken);

        if (user is null)
            return Result<string>.Failure(message: "User not found!");

        user.RemoveRefreshToken();
        await _userManager.UpdateAsync(user);
        return Result<string>.Success(user.Email!);
    }
}
