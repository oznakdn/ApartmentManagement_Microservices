using Financial.Domain.Enums;
using MediatR;

namespace Financial.Application.Commands.CreatePayment;

public record CreatePaymentRequest(string ExpenceItemId, int PaymentType) : IRequest<CreatePaymentResponse>;
