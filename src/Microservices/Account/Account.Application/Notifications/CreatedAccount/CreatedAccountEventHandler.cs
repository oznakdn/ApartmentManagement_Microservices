using Account.Domain.QueryEntities;
using Account.Infrastructure.Contexts;
using MediatR;

namespace Account.Application.Notifications.CreatedAccount;

public class CreatedAccountEventHandler : INotificationHandler<CreatedAccountEvent>
{
    private readonly QueryDbContext _dbContext;
    public CreatedAccountEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Handle(CreatedAccountEvent args, CancellationToken cancellationToken)
    {
        _dbContext.Users.Add(new UserQuery(
            args.UserId,
            args.FirstName,
            args.LastName,
            args.Address,
            args.PhoneNumber,
            args.Email,
            args.Picture,
            args.IsManager,
            args.IsResident,
            args.IsEmployee));

        await _dbContext.SaveChangesAsync();
    }
}
