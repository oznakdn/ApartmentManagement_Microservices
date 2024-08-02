using MediatR;

namespace Apartment.Application.Notifications.AssignedResidentToUnit;

public record AssignedResidentToUnitEvent(string UserId, string UnitId) : INotification;

