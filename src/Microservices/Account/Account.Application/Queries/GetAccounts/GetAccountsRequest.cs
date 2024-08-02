using MediatR;

namespace Account.Application.Queries.GetAccounts;

public record GetAccountsRequest() : IRequest<List<GetAccountsResponse>>;
