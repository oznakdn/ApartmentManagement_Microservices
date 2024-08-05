using MediatR;

namespace Financial.Application.Events.CreatedExpence;
public record CreatedExpenceEvent(string Id, string Title, string Description, decimal TotalAmount):INotification;

