using MediatR;

namespace Apartment.Application.Commands.CreateUnits;

public record CreateUnitsRequest(string BlockId) : IRequest<CreateUnitsResponse>;

