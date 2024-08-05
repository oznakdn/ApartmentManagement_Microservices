using MediatR;

namespace Financial.Application.Commands.CreateExpenceItems;

public record CreateExpenceItemsRequest(string ExpenceId, string[] UnitIds, DateTime LastPaymentDate) : IRequest<CreateExpenceItemsResponse>;

