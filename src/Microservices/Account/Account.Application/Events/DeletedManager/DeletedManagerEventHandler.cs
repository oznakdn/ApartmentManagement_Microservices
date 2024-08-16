using Account.Infrastructure.Contexts;
using MediatR;

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

        var user = _dbContext.Users.SingleOrDefault(x => x.Id == args.UserId);
        _dbContext.Users.Remove(user);
        var result = _dbContext.SaveChanges();

        if(result>0)
            await Task.CompletedTask;

    }
}
