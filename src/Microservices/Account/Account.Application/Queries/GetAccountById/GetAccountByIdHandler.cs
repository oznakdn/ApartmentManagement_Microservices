using Account.Application.Queries.GetAccounts;
using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Account.Application.Queries.GetAccountById;

public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdRequest, GetAccountsResponse>
{
    private readonly QueryDbContext _dbContext;
    public GetAccountByIdHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetAccountsResponse> Handle(GetAccountByIdRequest request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Users
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


        if (account is null)
            return null;


        return new GetAccountsResponse(account.Id,
            account.UserId,
            account.FullName,
            account.Email,
            account.PhoneNumber,
            account.Address,
            account.Picture,
            account.IsManager,
            account.IsEmployee,
            account.IsResident);

    }
}
