using Account.Domain.Entities;
using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Constants;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.AssignGuardRole;

public class AssignGuardRoleHandler : IRequestHandler<AssignGuardRoleRequest, IResult>
{
    private readonly CommandDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    public AssignGuardRoleHandler(CommandDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<IResult> Handle(AssignGuardRoleRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Where(x => x.Id == request.UserId)
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
            return Result.Failure(message: "User not found!");

        if (!await _userManager.IsInRoleAsync(user, RoleConstant.GUARD))
        {
            await _userManager.AddToRoleAsync(user, RoleConstant.GUARD);
        }


        return Result.Success(message: "Guard role assigned successfully.");

    }
}
