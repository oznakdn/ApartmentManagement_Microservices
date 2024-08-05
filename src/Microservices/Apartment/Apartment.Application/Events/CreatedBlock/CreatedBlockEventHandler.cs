using Apartment.Domain.Entities;
using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Application.Events.CreatedBlock;

public class CreatedBlockEventHandler : INotificationHandler<CreatedBlockEvent>
{
    private readonly QueryDbContext _dbContext;
    public CreatedBlockEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreatedBlockEvent args, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites.SingleOrDefaultAsync(x => x.Id == args.SiteId);
        var block = new Block(args.Name, args.SiteId, args.TotalUnits)
        {
            Id = args.Id
        };

        site.AddBlock(block);
        _dbContext.Blocks.Add(block);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
