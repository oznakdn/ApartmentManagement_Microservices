using Account.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Account.Application.Notifications.AssignedRole;

public class AssignedRoleEventHandler : INotificationHandler<AssignedRoleEvent>
{
    private readonly UserManager<User> _userManager;
    public AssignedRoleEventHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(AssignedRoleEvent args, CancellationToken cancellationToken)
    {
        if (!await _userManager.IsInRoleAsync(args.User, args.Role))
        {
            await _userManager.AddToRoleAsync(args.User, args.Role);

        }
    }
}
