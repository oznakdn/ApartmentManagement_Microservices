using MediatR;

namespace Apartment.Application.Queries.GetBlockCount;

public record GetBlockCountRequest() : IRequest<GetBlockCountResponse>;
