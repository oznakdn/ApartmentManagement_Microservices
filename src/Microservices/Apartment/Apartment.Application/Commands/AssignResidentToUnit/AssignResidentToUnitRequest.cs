using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Commands.AssignResidentToUnit;

public record AssignResidentToUnitRequest(string UserId, string UnitId) : IRequest<IResult>;
