using Financial.Infrastructure.Contexts;
using MediatR;

namespace Financial.Application.Events.CreatedExpenceItems;

public class CreatedExpenceItemsEventHandler : INotificationHandler<CreatedExpenceItemsEvent>
{
    private readonly ReadDbContext _dbContext;

    public CreatedExpenceItemsEventHandler(ReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreatedExpenceItemsEvent args, CancellationToken cancellationToken)
    {
        _dbContext.ExpenceItems.AddRange(args.ExpenceItems);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
