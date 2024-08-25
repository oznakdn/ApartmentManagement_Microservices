using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetSiteDetailReport;

public class GetSiteDetailReportHandler : IRequestHandler<GetSiteDetailReportRequest, IResult<GetSiteDetailReportResponse>>
{
    private readonly QueryDbContext _dbContext;
    public GetSiteDetailReportHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IResult<GetSiteDetailReportResponse>> Handle(GetSiteDetailReportRequest request, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites
            .AsNoTracking()
            .Where(x => x.Id == request.SiteId)
            .Include(x => x.Blocks)
            .ThenInclude(x => x.Units)
            .AsSplitQuery()
            .SingleOrDefaultAsync(cancellationToken);


        if (site is null)
            return Result<GetSiteDetailReportResponse>.Failure("Site not found!");



        int siteBlocks = site.Blocks.Count;

        int siteUnits = 0;
        int emptyUnits = 0;

        foreach (var block in site.Blocks)
        {
            siteUnits += block.Units.Count;


            foreach (var unit in block.Units)
            {
                if(unit.IsEmpty)
                    emptyUnits++;
            }

        }


        var result = new GetSiteDetailReportResponse(Blocks:siteBlocks, Units:siteUnits, EmptyUnits:emptyUnits, OccupiedUnits:siteUnits - emptyUnits);

        return Result<GetSiteDetailReportResponse>.Success(result);
    }
}
