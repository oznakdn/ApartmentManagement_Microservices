using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Account.Application.Events.Logined;

public class LoginedEventHandler : INotificationHandler<LoginedEvent>
{
    private readonly QueryDbContext _dbContext;
    public LoginedEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(LoginedEvent args, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == args.UserId)
            .SingleOrDefaultAsync(cancellationToken);

        user.SetRefreshToken(args.RefreshToken, args.RefreshExpire);
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
