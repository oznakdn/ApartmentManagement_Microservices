using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Queries.GetAccounts;

public record GetAccountsRequest() : IRequest<IResult<GetAccountsResponse>>;
