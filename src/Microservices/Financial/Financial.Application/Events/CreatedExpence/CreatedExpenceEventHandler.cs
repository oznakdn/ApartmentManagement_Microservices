using Financial.Domain.Entities;
using Financial.Infrastructure.Contexts;
using MediatR;

namespace Financial.Application.Events.CreatedExpence;

public class CreatedExpenceEventHandler : INotificationHandler<CreatedExpenceEvent>
{
    private readonly QueryDbContext _dbContext;
    public CreatedExpenceEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreatedExpenceEvent args, CancellationToken cancellationToken)
    {
        var expence = new Expence(args.Title, args.Description, args.TotalAmount)
        {
            Id = args.Id,
        };

        await _dbContext.Expences.AddAsync(expence, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
