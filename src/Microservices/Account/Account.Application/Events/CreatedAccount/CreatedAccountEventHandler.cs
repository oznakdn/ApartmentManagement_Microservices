using Account.Domain.Entities;
using Account.Infrastructure.Contexts;
using MediatR;

namespace Account.Application.Events.CreatedAccount;

public class CreatedAccountEventHandler : INotificationHandler<CreatedAccountEvent>
{
    private readonly QueryDbContext _dbContext;
    public CreatedAccountEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Handle(CreatedAccountEvent args, CancellationToken cancellationToken)
    {
        _dbContext.Users.Add(new User(
            args.FirstName,
            args.LastName,
            args.Address,
            args.PhoneNumber,
            args.Email,
            args.Picture,
            args.IsManager,
            args.IsResident,
            args.IsEmployee)
        {
            Id = args.Id
        });

        await _dbContext.SaveChangesAsync();
    }
}
