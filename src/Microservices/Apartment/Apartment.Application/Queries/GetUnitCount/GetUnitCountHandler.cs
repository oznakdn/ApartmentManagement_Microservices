using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Application.Queries.GetUnitCount;

public class GetUnitCountHandler : IRequestHandler<GetUnitCountRequest, GetUnitCountResponse>
{
    private readonly QueryDbContext _dbContext;
    public GetUnitCountHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetUnitCountResponse> Handle(GetUnitCountRequest request, CancellationToken cancellationToken)
    {
        var count = await _dbContext.Units.CountAsync(cancellationToken);
        return new GetUnitCountResponse(count);
    }
}
