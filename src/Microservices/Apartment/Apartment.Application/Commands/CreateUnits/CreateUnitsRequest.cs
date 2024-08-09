using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Commands.CreateUnits;

public record CreateUnitsRequest(string BlockId) : IRequest<IResult>;

