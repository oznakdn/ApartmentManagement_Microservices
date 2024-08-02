using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Application.Queries.GetSiteByManagerId;

public class GetSiteByManagerIdHandler : IRequestHandler<GetSiteByManagerIdRequest, GetSiteByManagerIdResponse>
{
    private readonly QueryDbContext _dbContext;
    public GetSiteByManagerIdHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetSiteByManagerIdResponse> Handle(GetSiteByManagerIdRequest request, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites
            .SingleOrDefaultAsync(x => x.ManagerId == request.ManagerId, cancellationToken);

        if (site is null)
            return null;

        return new GetSiteByManagerIdResponse(site.SiteId, site.Name,site.Address);

    }
}
