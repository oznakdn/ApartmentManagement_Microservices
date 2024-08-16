using Account.Application.Events.DeletedManager;
using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;
using Shared.Core.MessageQueue.Models;
using Shared.Core.MessageQueue.Queues;
using Shared.MessagePublising;

namespace Account.Application.Commands.DeleteManager;

public class DeleteManagerHandler : IRequestHandler<DeleteManagerRequest, IResult>
{
    private readonly CommandDbContext _dbContext;
    private readonly IMessagePublisher _publisher;
    private readonly IMediator _eventHandler;
    public DeleteManagerHandler(CommandDbContext dbContext, IMediator eventHandler, IMessagePublisher publisher)
    {
        _dbContext = dbContext;
        _eventHandler = eventHandler;
        _publisher = publisher;
    }

    public async Task<IResult> Handle(DeleteManagerRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user is null)
            return Result.Failure(message: "User not found!");


        string siteId = user.SiteId!;
        _dbContext.Users.Remove(user);


        var isSuccess = _eventHandler.Publish(new DeletedManagerEvent(user.Id));

        if (isSuccess.IsCompletedSuccessfully)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(siteId))
            {
                await _publisher.PublishAsync(queue: SiteQueue.DELETE_MANAGER, messageBody: new DeleteManagerFromSiteModel
                {
                    ManagerId = user.Id,
                    SiteId = siteId
                });
            }


            return Result.Success(message: "User deleted successfully!");
        }

        return Result.Failure(message: "Failed to delete user!");


        
    }
}
