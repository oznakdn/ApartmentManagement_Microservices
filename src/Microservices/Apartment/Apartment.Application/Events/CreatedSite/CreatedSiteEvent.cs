using MediatR;

namespace Apartment.Application.Events.CreatedSite;

public record CreatedSiteEvent(string Id, string? ManagerId, string Name, string Address):INotification;

