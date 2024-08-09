using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetSiteByManagerId;

public record GetSiteByManagerIdRequest(string ManagerId) : IRequest<IResult<GetSiteByManagerIdResponse>>;

