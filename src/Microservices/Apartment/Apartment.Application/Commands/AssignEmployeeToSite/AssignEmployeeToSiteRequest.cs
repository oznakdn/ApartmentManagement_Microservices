using MediatR;

namespace Apartment.Application.Commands.AssignEmployeeToSite;

public record AssignEmployeeToSiteRequest(string UserId, string SiteId) : IRequest<AssignEmployeeToSiteResponse>;

