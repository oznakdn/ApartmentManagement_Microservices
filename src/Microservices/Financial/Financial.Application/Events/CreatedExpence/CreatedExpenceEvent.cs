using MediatR;

namespace Financial.Application.Events.CreatedExpence;
public record CreatedExpenceEvent(string Id, string SiteId, string Title, string Description, decimal TotalAmount) : INotification;

