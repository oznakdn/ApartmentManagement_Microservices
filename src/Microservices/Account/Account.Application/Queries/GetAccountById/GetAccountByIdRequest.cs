using Account.Application.Queries.GetAccounts;
using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Queries.GetAccountById;

public record GetAccountByIdRequest(string Id) : IRequest<IResult<GetAccountsResponse>>;
