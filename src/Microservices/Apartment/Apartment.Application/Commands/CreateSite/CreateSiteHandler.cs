using Apartment.Application.Events.CreatedSite;
using Apartment.Domain.Entities;
using Apartment.Infrastructure.Context;
using MediatR;
using Shared.Core.MessageQueue.Models;
using Shared.Core.MessageQueue.Queues;
using Shared.MessagePublising;


namespace Apartment.Application.Commands.CreateSite;

public class CreateSiteHandler : IRequestHandler<CreateSiteRequest, CreateSiteResponse>
{
    private readonly CommandDbContext _dbContext;
    private readonly IMediator _notification;
    private readonly IMessagePublisher _publisher;

    public CreateSiteHandler(CommandDbContext dbContext, IMediator notification, IMessagePublisher publisher)
    {
        _dbContext = dbContext;
        _notification = notification;
        _publisher = publisher;
    }

    public async Task<CreateSiteResponse> Handle(CreateSiteRequest request, CancellationToken cancellationToken)
    {
        var site = new Site(request.ManagerId, request.Name, request.Address);

        _dbContext.Sites.Add(site);
        await _dbContext.SaveChangesAsync(cancellationToken);


        await _notification.Publish(new CreatedSiteEvent(site.Id, site.ManagerId, site.Name, site.Address));

        if (!string.IsNullOrWhiteSpace(request.ManagerId))
        {
            await _publisher.PublishAsync<CreateSiteModel>(SiteQueue.SITE_CREATED, new CreateSiteModel
            {
                ManagerId = request.ManagerId,
                SiteId = site.Id
            });

        }

        return new CreateSiteResponse(true, "Site created successfully");

    }
}
