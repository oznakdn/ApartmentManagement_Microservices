using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Commands.CreateSite;

public record CreateSiteRequest(string? ManagerId, string Name, string Address) : IRequest<IResult>;

