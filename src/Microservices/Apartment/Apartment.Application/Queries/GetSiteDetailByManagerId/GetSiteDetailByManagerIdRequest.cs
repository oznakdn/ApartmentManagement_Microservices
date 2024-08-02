using MediatR;

namespace Apartment.Application.Queries.GetSiteDetailByManagerId;

public record GetSiteDetailByManagerIdRequest(string ManagerId) : IRequest<GetSiteDetailByManagerIdResponse>;

