using Apartment.Domain.Entities;
using Apartment.Infrastructure.Context;
using MediatR;

namespace Apartment.Application.Events.CreatedSite;

public class CreatedSiteEventHandler : INotificationHandler<CreatedSiteEvent>
{
    private readonly QueryDbContext _dbContext;
    public CreatedSiteEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreatedSiteEvent args, CancellationToken cancellationToken)
    {
        var site = new Site(args.ManagerId, args.Name, args.Address)
        {
            Id = args.Id
        };
   
        _dbContext.Sites.Add(site);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
