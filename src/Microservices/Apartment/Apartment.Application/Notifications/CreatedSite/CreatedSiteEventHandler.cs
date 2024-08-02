using Apartment.Domain.QueryEntities;
using Apartment.Infrastructure.Context;
using MediatR;

namespace Apartment.Application.Notifications.CreatedSite;

public class CreatedSiteEventHandler : INotificationHandler<CreatedSiteEvent>
{
    private readonly QueryDbContext _dbContext;
    public CreatedSiteEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreatedSiteEvent args, CancellationToken cancellationToken)
    {
        var site = new SiteQuery(args.SiteId, args.ManagerId, args.Name, args.Address);
   
        _dbContext.Sites.Add(site);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
