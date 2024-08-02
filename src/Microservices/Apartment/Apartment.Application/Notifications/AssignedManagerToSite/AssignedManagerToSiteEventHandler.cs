using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Application.Notifications.AssignedManagerToSite;

public class AssignedManagerToSiteEventHandler : INotificationHandler<AssignedManagerToSiteEvent>
{
    private readonly QueryDbContext _dbContext;
    public AssignedManagerToSiteEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(AssignedManagerToSiteEvent args, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites.SingleOrDefaultAsync(x => x.SiteId == args.SiteId, cancellationToken);
        site.ManagerId = args.ManagerId;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
