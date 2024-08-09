using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetSiteDetailByManagerId;

public class GetSiteDetailByManagerIdHandler : IRequestHandler<GetSiteDetailByManagerIdRequest, IResult<GetSiteDetailByManagerIdResponse>>
{
    private readonly QueryDbContext _dbContext;
    public GetSiteDetailByManagerIdHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IResult<GetSiteDetailByManagerIdResponse>> Handle(GetSiteDetailByManagerIdRequest request, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites.Include(x=>x.Blocks).ThenInclude(x=>x.Units)
            .SingleOrDefaultAsync(x => x.ManagerId == request.ManagerId, cancellationToken);

        if (site is null)
            return Result<GetSiteDetailByManagerIdResponse>.Failure("Site not found!");

        var result = site.Map();
        return Result<GetSiteDetailByManagerIdResponse>.Success(result);
        
    }
}
