using Account.Domain.Entities;
using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Constants;

namespace Account.Application.Commands.AssignGuardRole;

public class AssignGuardRoleHandler : IRequestHandler<AssignGuardRoleRequest, AssignGuardRoleResponse>
{
    private readonly CommandDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    public AssignGuardRoleHandler(CommandDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<AssignGuardRoleResponse> Handle(AssignGuardRoleRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Where(x => x.Id == request.UserId)
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
            return new AssignGuardRoleResponse(false, "User not found!");

        if (!await _userManager.IsInRoleAsync(user, RoleConstant.GUARD))
        {
            await _userManager.AddToRoleAsync(user, RoleConstant.GUARD);
        }

        return new AssignGuardRoleResponse(true, "Guard role assigned successfully!");


    }
}
