using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Commands.AssignManagerToSite;

public record AssignManagerToSiteRequest(string UserId, string SiteId) : IRequest<IResult>;

