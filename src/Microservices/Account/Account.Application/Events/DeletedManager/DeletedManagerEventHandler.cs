using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Account.Application.Events.DeletedManager;

public class DeletedManagerEventHandler : INotificationHandler<DeletedManagerEvent>
{
    private readonly QueryDbContext _dbContext;

    public DeletedManagerEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeletedManagerEvent args, CancellationToken cancellationToken)
    {

        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == args.UserId, cancellationToken);
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
