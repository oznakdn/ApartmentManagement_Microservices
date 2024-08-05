using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Application.Events.AssignedResidentToUnit;

public class AssignedResidentToUnitEventHandler : INotificationHandler<AssignedResidentToUnitEvent>
{
    private readonly QueryDbContext _dbContext;
    public AssignedResidentToUnitEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(AssignedResidentToUnitEvent args, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Units.SingleOrDefaultAsync(x => x.Id == args.UnitId, cancellationToken);
        site.AssignResident(args.UserId);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
