using MediatR;

namespace Apartment.Application.Events.AssignedManagerToSite;

public record AssignedManagerToSiteEvent(string SiteId, string ManagerId) : INotification;

