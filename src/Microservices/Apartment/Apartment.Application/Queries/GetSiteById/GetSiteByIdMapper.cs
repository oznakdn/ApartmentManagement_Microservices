using Apartment.Domain.Entities;

namespace Apartment.Application.Queries.GetSiteById;

public static class GetSiteByIdMapper
{
    public static GetSiteByIdResponse Map(this Site site)
    {
        return new GetSiteByIdResponse(site.Id, site.Name, site.Address);
    }
}
