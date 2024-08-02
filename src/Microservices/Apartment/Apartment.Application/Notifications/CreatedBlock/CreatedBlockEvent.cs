using MediatR;

namespace Apartment.Application.Notifications.CreatedBlock;

public record CreatedBlockEvent(string SiteId, string BlockId, string Name,int TotalUnits) : INotification;

