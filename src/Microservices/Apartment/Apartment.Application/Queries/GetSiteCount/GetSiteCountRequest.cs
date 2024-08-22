using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetSiteCount;

public record GetSiteCountRequest() : IRequest<GetSiteCountResponse>;

