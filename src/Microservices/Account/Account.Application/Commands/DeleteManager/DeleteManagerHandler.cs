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

        _dbContext.Users.Remove(user);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if(result == 0)
            return Result.Failure(message: "User not deleted!");

        await _eventHandler.Publish(new DeletedManagerEvent(user.Id));

        if (!string.IsNullOrWhiteSpace(user.SiteId))
        {
            await _publisher.PublishAsync(queue: SiteQueue.DELETE_MANAGER, messageBody: new DeleteManagerFromSiteModel
            {
                ManagerId = user.Id,
                SiteId = user.SiteId
            });
        }
            

        return Result.Success(message: "User deleted successfully!");
    }
}
