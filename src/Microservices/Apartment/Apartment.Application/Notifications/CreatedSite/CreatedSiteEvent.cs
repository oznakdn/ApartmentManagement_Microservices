using MediatR;

namespace Apartment.Application.Notifications.CreatedSite;

public record CreatedSiteEvent(string SiteId, string? ManagerId, string Name, string Address):INotification;

