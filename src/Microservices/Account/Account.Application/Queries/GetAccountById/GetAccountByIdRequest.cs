using Account.Application.Queries.GetAccounts;
using MediatR;

namespace Account.Application.Queries.GetAccountById;

public record GetAccountByIdRequest(string Id) : IRequest<GetAccountsResponse>;
