using MediatR;

namespace Apartment.Application.Notifications.AssignedManagerToSite;

public record AssignedManagerToSiteEvent(string SiteId, string ManagerId) : INotification;

