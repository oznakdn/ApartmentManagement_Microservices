using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Account.Application.Events.RefreshLogined;

public class RefreshLoginedEventHandler : INotificationHandler<RefreshLoginedEvent>
{
    private readonly QueryDbContext _dbContext;
    public RefreshLoginedEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(RefreshLoginedEvent args, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .SingleOrDefaultAsync(x=>x.RefreshToken == args.RefreshToken, cancellationToken);

        user!.SetRefreshToken(args.NewRefreshToken, args.RefreshExpire);
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
