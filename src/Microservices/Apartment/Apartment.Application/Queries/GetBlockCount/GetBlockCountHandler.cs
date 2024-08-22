using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Application.Queries.GetBlockCount;

public class GetBlockCountHandler : IRequestHandler<GetBlockCountRequest, GetBlockCountResponse>
{
    private readonly QueryDbContext _dbContext;

    public GetBlockCountHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetBlockCountResponse> Handle(GetBlockCountRequest request, CancellationToken cancellationToken)
    {
        var count = await _dbContext.Blocks.CountAsync(cancellationToken);
        return new GetBlockCountResponse(count);
    }
}
