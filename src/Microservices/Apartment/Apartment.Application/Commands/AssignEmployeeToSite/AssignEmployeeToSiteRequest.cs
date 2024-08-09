using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Commands.AssignEmployeeToSite;

public record AssignEmployeeToSiteRequest(string UserId, string SiteId) : IRequest<IResult>;

