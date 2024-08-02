using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Application.Notifications.CreatedUnits;

public class CreatedUnitsEventHandler : INotificationHandler<CreatedUnitsEvent>
{
    private readonly QueryDbContext _dbContext;

    public CreatedUnitsEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreatedUnitsEvent args, CancellationToken cancellationToken)
    {
        var block = await _dbContext.Blocks.SingleOrDefaultAsync(x => x.BlockId == args.BlockId, cancellationToken);

        var unit = new Domain.QueryEntities.UnitQuery(null, args.UnitId, args.BlockId, args.UnitNo, hasCar: false);
        block.AddUnit(unit);

        _dbContext.Units.Add(unit);
        await _dbContext.SaveChangesAsync(cancellationToken);

    }
}
