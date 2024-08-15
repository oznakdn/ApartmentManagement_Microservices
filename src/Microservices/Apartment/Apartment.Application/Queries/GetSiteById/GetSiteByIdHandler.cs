using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetSiteById;

public class GetSiteByIdHandler : IRequestHandler<GetSiteByIdRequest, IResult<GetSiteByIdResponse>>
{
    private readonly QueryDbContext _dbContext;
    public GetSiteByIdHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IResult<GetSiteByIdResponse>> Handle(GetSiteByIdRequest request, CancellationToken cancellationToken)
    {
        var site = await _dbContext.Sites
            .SingleOrDefaultAsync(x => x.Id == request.SiteId, cancellationToken);

        if(site is null)
            return Result<GetSiteByIdResponse>.Failure(message: "Site not found!");

        var data = site.Map();
        return Result<GetSiteByIdResponse>.Success(data);
    }
}
