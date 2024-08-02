using MediatR;

namespace Apartment.Application.Notifications.CreatedUnits;

public record CreatedUnitsEvent(string UnitId, string BlockId, int UnitNo) : INotification;

