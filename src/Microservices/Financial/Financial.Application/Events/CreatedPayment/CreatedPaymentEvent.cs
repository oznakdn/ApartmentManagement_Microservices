using MediatR;

namespace Financial.Application.Events.CreatedPayment;

public record CreatedPaymentEvent(string Id, string ExpenceItemId, int PaymentType) : INotification;

