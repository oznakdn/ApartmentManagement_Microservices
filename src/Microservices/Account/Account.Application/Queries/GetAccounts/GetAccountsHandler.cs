using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Account.Application.Queries.GetAccounts;

public class GetAccountsHandler : IRequestHandler<GetAccountsRequest, IResult<GetAccountsResponse>>
{
    private readonly QueryDbContext _dbContext;
    public GetAccountsHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IResult<GetAccountsResponse>> Handle(GetAccountsRequest request, CancellationToken cancellationToken)
    {
        var accounts = await _dbContext.Users.ToListAsync(cancellationToken);

        var result = accounts.Map();

        return Result<GetAccountsResponse>.Success(values:result);
    }
}
