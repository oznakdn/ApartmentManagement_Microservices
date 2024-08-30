using Apartment.Application.Events.AssignedManagerToSite;
using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;
using Shared.Core.Models;
using Shared.MessagePublising;

namespace Apartment.Application.Commands.AssignManagerToSite;

public class AssignManagerToSiteHandler : IRequestHandler<AssignManagerToSiteRequest, IResult>
{
    private readonly CommandDbContext _dbContext;
    private readonly IMediator _notification;
    private readonly IMessagePublisher _publisher;

    public AssignManagerToSiteHandler(CommandDbContext dbContext, IMediator notification, IMessagePublisher publisher)
    {
        _dbContext = dbContext;
        _notification = notification;
        _publisher = publisher;
    }

    public async Task<IResult> Handle(AssignManagerToSiteRequest request, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites
            .SingleOrDefaultAsync(x => x.Id == request.SiteId, cancellationToken);

        if (site is null)
            return Result.Failure(message: "Site not found!");


        site.AssignManager(request.UserId);
        _dbContext.Sites.Update(site);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result == 0)
            return Result.Failure(message: "Failed to assign manager to site!");


        await _notification.Publish(new AssignedManagerToSiteEvent(site.Id, request.UserId));

        await _publisher.PublishAsync<AssignManagerToSiteModel>(queue: QueueConstant.ASSIGN_MANAGER, messageBody: new AssignManagerToSiteModel
        {
            SiteId = site.Id,
            ManagerId = request.UserId
        });


        return Result.Success(message: "Manager assigned to site");


    }
}
