using Apartment.Domain.QueryEntities;
using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Application.Notifications.CreatedBlock;

public class CreatedBlockEventHandler : INotificationHandler<CreatedBlockEvent>
{
    private readonly QueryDbContext _dbContext;
    public CreatedBlockEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreatedBlockEvent args, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites.SingleOrDefaultAsync(x => x.SiteId == args.SiteId);
        var block = new BlockQuery(args.BlockId, args.Name, args.SiteId, args.TotalUnits);

        site.AddBlock(block);
        _dbContext.Blocks.Add(block);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
