using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Account.Application.Queries.GetManagers;

public class GetManagersHandler : IRequestHandler<GetManagersRequest, IResult<GetManagersResponse>>
{
    private readonly QueryDbContext _dbContext;

    public GetManagersHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IResult<GetManagersResponse>> Handle(GetManagersRequest request, CancellationToken cancellationToken)
    {
        var managers = await _dbContext.Users.Where(x => x.IsManager).ToListAsync();
        var result = managers.Map();
        return Result<GetManagersResponse>.Success(result);
    }
}
