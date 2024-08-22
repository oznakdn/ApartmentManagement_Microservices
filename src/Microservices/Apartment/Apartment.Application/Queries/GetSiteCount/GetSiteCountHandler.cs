using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Application.Queries.GetSiteCount;

public class GetSiteCountHandler : IRequestHandler<GetSiteCountRequest, GetSiteCountResponse>
{
    private readonly QueryDbContext _dbContext;
    public GetSiteCountHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetSiteCountResponse> Handle(GetSiteCountRequest request, CancellationToken cancellationToken)
    {
        var count = await _dbContext.Sites.CountAsync(cancellationToken);
        return new GetSiteCountResponse(Count: count);
    }
}
