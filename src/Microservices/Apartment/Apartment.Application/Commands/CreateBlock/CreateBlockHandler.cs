using Apartment.Application.Events.CreatedBlock;
using Apartment.Domain.Entities;
using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Application.Commands.CreateBlock;

public class CreateBlockHandler : IRequestHandler<CreateBlockRequest, CreateBlockResponse>
{
    private readonly CommandDbContext _dbContext;
    private readonly IMediator _notification;
    public CreateBlockHandler(CommandDbContext dbContext, IMediator notification)
    {
        _dbContext = dbContext;
        _notification = notification;
    }

    public async Task<CreateBlockResponse> Handle(CreateBlockRequest request, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites.SingleOrDefaultAsync(x => x.Id == request.SiteId, cancellationToken);
        if (site == null)
            return new CreateBlockResponse(false, "Site not found");

        var block = new Block(request.Name, request.SiteId, request.TotalUnits);
        site.AddBlock(block);
        _dbContext.Blocks.Add(block);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        if (result < 0)
            return new CreateBlockResponse(false, "Failed to create block");

        await _notification.Publish(new CreatedBlockEvent(request.SiteId, block.Id, block.Name, block.TotalUnits));

        return new CreateBlockResponse(true, "Block created successfully");
    }
}
