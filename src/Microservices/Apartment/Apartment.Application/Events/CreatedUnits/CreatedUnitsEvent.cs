using MediatR;

namespace Apartment.Application.Events.CreatedUnits;

public record CreatedUnitsEvent(string Id, string BlockId, int UnitNo) : INotification;

