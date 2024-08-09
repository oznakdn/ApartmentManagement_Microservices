using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetSiteDetailByManagerId;

public record GetSiteDetailByManagerIdRequest(string ManagerId) : IRequest<IResult<GetSiteDetailByManagerIdResponse>>;

