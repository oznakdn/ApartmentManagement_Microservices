using Google.Protobuf.Collections;
using MediatR;
using Shared.Core.Interfaces;

namespace Financial.Application.Commands.CreateExpenceItems;

public record CreateExpenceItemsRequest(string ExpenceId, RepeatedField<string> UnitIds, string LastPaymentDate) : IRequest<IResult>;

