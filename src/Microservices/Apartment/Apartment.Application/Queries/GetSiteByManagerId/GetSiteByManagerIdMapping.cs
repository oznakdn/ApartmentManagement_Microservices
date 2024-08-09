using Apartment.Domain.Entities;

namespace Apartment.Application.Queries.GetSiteByManagerId;

public static class GetSiteByManagerIdMapping
{
    public static GetSiteByManagerIdResponse Map(this Site site)
    {
        return new GetSiteByManagerIdResponse(site.Id, site.Name, site.Address);
    }
}
