using Account.Application.Queries.GetManagers;
using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Account.Application.Queries.GetManagerById;

public class GetManagerByIdHandler : IRequestHandler<GetManagerByIdRequest, IResult<GetManagersResponse>>
{
    private readonly QueryDbContext _dbContext;
    public GetManagerByIdHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IResult<GetManagersResponse>> Handle(GetManagerByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .SingleOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if(user is null)
            return Result<GetManagersResponse>.Failure("User not found!");

        var data = user.Map();
        return Result<GetManagersResponse>.Success(data);
    }
}
