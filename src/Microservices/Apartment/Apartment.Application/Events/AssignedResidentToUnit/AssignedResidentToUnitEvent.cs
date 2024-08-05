using MediatR;

namespace Apartment.Application.Events.AssignedResidentToUnit;

public record AssignedResidentToUnitEvent(string UserId, string UnitId) : INotification;

