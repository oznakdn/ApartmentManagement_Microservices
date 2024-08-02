using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.MessageQueue.Models;
using Shared.Core.MessageQueue.Queues;
using Shared.MessagePublising;

namespace Apartment.Application.Commands.AssignEmployeeToSite;

public class AssignEmployeeToSiteHandler : IRequestHandler<AssignEmployeeToSiteRequest, AssignEmployeeToSiteResponse>
{
    private readonly CommandDbContext _dbContext;
    private readonly IMessagePublisher _publisher;

    public AssignEmployeeToSiteHandler(CommandDbContext dbContext, IMessagePublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task<AssignEmployeeToSiteResponse> Handle(AssignEmployeeToSiteRequest request, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites
            .SingleOrDefaultAsync(x => x.Id == request.SiteId, cancellationToken);

        if (site is null)
            return new AssignEmployeeToSiteResponse(false, "Site not found!");


        await _publisher.PublishAsync<AssignEmployeeToSiteModel>(queue: SiteQueue.ASSIGN_EMPLOYEE, messageBody: new AssignEmployeeToSiteModel
        {
            SiteId = site.Id,
            EmployeeId = request.UserId
        });


        return new AssignEmployeeToSiteResponse(true, "Employee assigned to site");
    }
}
