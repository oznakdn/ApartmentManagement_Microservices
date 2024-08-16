using Apartment.Domain.Entities;

namespace Apartment.Application.Queries.GetAllSite;

public static class GetAllSiteMapper
{
    public static List<GetAllSiteResponse> Map(this List<Site> sites)
    {
        return sites.Select(x => new GetAllSiteResponse(x.Id, x.ManagerId, x.Name, x.Address)).ToList();
    }
}
