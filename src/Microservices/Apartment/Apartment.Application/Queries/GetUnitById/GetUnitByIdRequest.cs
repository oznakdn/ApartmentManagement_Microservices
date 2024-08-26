using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetUnitById;

public record GetUnitByIdRequest(string UnitId) : IRequest<IResult<GetUnitByIdResponse>>;
