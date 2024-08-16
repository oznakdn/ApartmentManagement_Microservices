using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetAllSite;

public record GetAllSiteRequest() : IRequest<IResult<GetAllSiteResponse>>;

