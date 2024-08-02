using MediatR;

namespace Apartment.Application.Commands.AssignResidentToUnit;

public record AssignResidentToUnitRequest(string UserId, string UnitId) : IRequest<AssignResidentToUnitResponse>;
