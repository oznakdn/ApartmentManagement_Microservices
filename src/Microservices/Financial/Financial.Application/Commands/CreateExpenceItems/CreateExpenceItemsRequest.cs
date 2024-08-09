using MediatR;
using Shared.Core.Interfaces;

namespace Financial.Application.Commands.CreateExpenceItems;

public record CreateExpenceItemsRequest(string ExpenceId, string[] UnitIds, DateTime LastPaymentDate) : IRequest<IResult>;

