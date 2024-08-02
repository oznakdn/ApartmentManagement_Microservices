using Account.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Account.Application.Commands.Logout;

public class LogoutHandler : IRequestHandler<LogoutRequest, LogoutResponse>
{
    private readonly UserManager<User> _userManager;
    public LogoutHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<LogoutResponse> Handle(LogoutRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.RefreshToken == request.RefreshToken);

        if (user is null)
            return new LogoutResponse(false);

        user.RemoveRefreshToken();
        await _userManager.UpdateAsync(user);
        return new LogoutResponse(true);
    }
}
