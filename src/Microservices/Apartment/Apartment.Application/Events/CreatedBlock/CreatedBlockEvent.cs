using MediatR;

namespace Apartment.Application.Events.CreatedBlock;

public record CreatedBlockEvent(string SiteId, string Id, string Name,int TotalUnits) : INotification;

