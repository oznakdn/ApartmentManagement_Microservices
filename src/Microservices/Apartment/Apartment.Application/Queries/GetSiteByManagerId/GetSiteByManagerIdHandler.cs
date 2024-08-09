using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetSiteByManagerId;

public class GetSiteByManagerIdHandler : IRequestHandler<GetSiteByManagerIdRequest, IResult<GetSiteByManagerIdResponse>>
{
    private readonly QueryDbContext _dbContext;
    public GetSiteByManagerIdHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IResult<GetSiteByManagerIdResponse>> Handle(GetSiteByManagerIdRequest request, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites
            .SingleOrDefaultAsync(x => x.ManagerId == request.ManagerId, cancellationToken);

        if (site is null)
            return Result<GetSiteByManagerIdResponse>.Failure(message: "Site not found!");

        var result = site.Map();
        return Result<GetSiteByManagerIdResponse>.Success(result);

    }
}
