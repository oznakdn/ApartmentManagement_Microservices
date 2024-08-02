using Apartment.Domain.QueryEntities;
using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Application.Queries.GetSiteDetailByManagerId;

public class GetSiteDetailByManagerIdHandler : IRequestHandler<GetSiteDetailByManagerIdRequest, GetSiteDetailByManagerIdResponse>
{
    private readonly QueryDbContext _dbContext;
    public GetSiteDetailByManagerIdHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetSiteDetailByManagerIdResponse> Handle(GetSiteDetailByManagerIdRequest request, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites.Include(x=>x.Blocks).ThenInclude(x=>x.Units)
            .SingleOrDefaultAsync(x => x.ManagerId == request.ManagerId, cancellationToken);

        if (site is null)
            return null;


        return new GetSiteDetailByManagerIdResponse(
            site.SiteId,
            site.Name,
            site.Address,
            site.Blocks.Select(x => new GetSiteDetailBlocks(x.Name, x.TotalUnits, x.AvailableUnits,
            x.Units.Select(y => new GetSiteDetailUnits(y.UnitNo, y.IsEmpty, y.HasCar, y.ResidentId)).ToList())).ToList());
    }
}
