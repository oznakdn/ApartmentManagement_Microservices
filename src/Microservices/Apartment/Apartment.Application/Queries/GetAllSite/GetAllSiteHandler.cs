using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetAllSite;

public class GetAllSiteHandler : IRequestHandler<GetAllSiteRequest, IResult<GetAllSiteResponse>>
{
    private readonly QueryDbContext _dbContext;
    public GetAllSiteHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IResult<GetAllSiteResponse>> Handle(GetAllSiteRequest request, CancellationToken cancellationToken)
    {
        var sites = await _dbContext.Sites.ToListAsync(cancellationToken);

        var datas = sites.Map();
        return Result<GetAllSiteResponse>.Success(datas);
    }
}
