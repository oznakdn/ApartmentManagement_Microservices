using Account.Application.Queries.GetAccounts;
using Account.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Account.Application.Queries.GetAccountById;

public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdRequest, IResult<GetAccountsResponse>>
{
    private readonly QueryDbContext _dbContext;
    public GetAccountByIdHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IResult<GetAccountsResponse>> Handle(GetAccountByIdRequest request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Users
           .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


        if (account is null)
            return Result<GetAccountsResponse>.Failure(message: "Account not found");

        var result = account.Map();

        return Result<GetAccountsResponse>.Success(result);
    }
}
