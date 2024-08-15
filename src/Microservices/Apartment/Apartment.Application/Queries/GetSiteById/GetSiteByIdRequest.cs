using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetSiteById;

public record GetSiteByIdRequest(string SiteId) : IRequest<IResult<GetSiteByIdResponse>>;

