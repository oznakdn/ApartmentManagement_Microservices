using MediatR;

namespace Financial.Application.Queries.GetNonPaidExpenceItems;

public record GetNonPaidExpenceItemsRequest(string ExpenceId) : IRequest<List<GetNonPaidExpenceItemsResponse>>;

