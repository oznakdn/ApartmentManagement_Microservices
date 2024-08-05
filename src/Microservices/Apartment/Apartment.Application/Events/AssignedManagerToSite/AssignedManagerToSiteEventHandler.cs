using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Application.Events.AssignedManagerToSite;

public class AssignedManagerToSiteEventHandler : INotificationHandler<AssignedManagerToSiteEvent>
{
    private readonly QueryDbContext _dbContext;
    public AssignedManagerToSiteEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(AssignedManagerToSiteEvent args, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites.SingleOrDefaultAsync(x => x.Id == args.SiteId, cancellationToken);
        site.AssignManager(args.ManagerId);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
