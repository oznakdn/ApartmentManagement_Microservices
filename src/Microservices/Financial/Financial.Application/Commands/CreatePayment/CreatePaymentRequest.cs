using MediatR;
using Shared.Core.Interfaces;

namespace Financial.Application.Commands.CreatePayment;

public record CreatePaymentRequest(string ExpenceItemId, int PaymentType) : IRequest<IResult>;
