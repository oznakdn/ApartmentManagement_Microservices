using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Account.Application.Queries.GetAccounts;

public class GetAccountsHandler : IRequestHandler<GetAccountsRequest, List<GetAccountsResponse>>
{
    private readonly QueryDbContext _dbContext;
    public GetAccountsHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetAccountsResponse>> Handle(GetAccountsRequest request, CancellationToken cancellationToken)
    {
        var accounts = await _dbContext.Users.ToListAsync(cancellationToken);
        return accounts.Select(x => new GetAccountsResponse(
            x.Id,
            x.FullName,
            x.Email,
            x.PhoneNumber,
            x.Address,
            x.Picture,
            x.IsManager,
            x.IsEmployee,
            x.IsResident
            )).ToList();
    }
}
