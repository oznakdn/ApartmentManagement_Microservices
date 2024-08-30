using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;
using Shared.Core.Models;
using Shared.MessagePublising;

namespace Apartment.Application.Commands.AssignEmployeeToSite;

public class AssignEmployeeToSiteHandler : IRequestHandler<AssignEmployeeToSiteRequest, IResult>
{
    private readonly CommandDbContext _dbContext;
    private readonly IMessagePublisher _publisher;

    public AssignEmployeeToSiteHandler(CommandDbContext dbContext, IMessagePublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task<IResult> Handle(AssignEmployeeToSiteRequest request, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites
            .SingleOrDefaultAsync(x => x.Id == request.SiteId, cancellationToken);

        if (site is null)
            return Result.Failure(message: "Site not found!");


        await _publisher.PublishAsync<AssignEmployeeToSiteModel>(queue: QueueConstant.ASSIGN_EMPLOYEE, messageBody: new AssignEmployeeToSiteModel
        {
            SiteId = site.Id,
            EmployeeId = request.UserId
        });


        return Result.Success(message: "Employee assigned to site");
    }
}
