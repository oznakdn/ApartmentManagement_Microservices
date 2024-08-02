using MediatR;

namespace Apartment.Application.Commands.AssignManagerToSite;

public record AssignManagerToSiteRequest(string UserId, string SiteId) : IRequest<AssignManagerToSiteResponse>;

