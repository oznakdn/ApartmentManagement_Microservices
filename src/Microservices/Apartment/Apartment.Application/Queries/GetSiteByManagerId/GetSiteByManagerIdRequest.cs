using MediatR;

namespace Apartment.Application.Queries.GetSiteByManagerId;

public record GetSiteByManagerIdRequest(string ManagerId) : IRequest<GetSiteByManagerIdResponse>;

