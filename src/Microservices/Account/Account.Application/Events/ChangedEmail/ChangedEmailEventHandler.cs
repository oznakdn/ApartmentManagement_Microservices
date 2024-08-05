using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Account.Application.Events.ChangedEmail;

public class ChangedEmailEventHandler : INotificationHandler<ChangedEmailEvent>
{
    private readonly QueryDbContext _dbContext;
    public ChangedEmailEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(ChangedEmailEvent args, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Users
            .SingleOrDefaultAsync(x => x.Id == args.UserId, cancellationToken);


        account!.ChangeEmail(args.NewEmail);
        _dbContext.Users.Update(account);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
