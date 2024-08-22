using MediatR;

namespace Apartment.Application.Queries.GetUnitCount;

public record GetUnitCountRequest() : IRequest<GetUnitCountResponse>;
