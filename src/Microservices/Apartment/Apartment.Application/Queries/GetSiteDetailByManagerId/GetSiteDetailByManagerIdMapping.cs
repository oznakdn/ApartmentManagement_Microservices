using Apartment.Domain.Entities;

namespace Apartment.Application.Queries.GetSiteDetailByManagerId;

public static class GetSiteDetailByManagerIdMapping
{
    public static GetSiteDetailByManagerIdResponse Map(this Site site)
    {
        return new GetSiteDetailByManagerIdResponse(
            site.Id,
            site.Name,
            site.Address,
            site.Blocks.Select(x => new GetSiteDetailBlocks(x.Name, x.TotalUnits, x.AvailableUnits,
            x.Units.Select(y => new GetSiteDetailUnits(y.UnitNo, y.IsEmpty, y.HasCar, y.ResidentId)).ToList())).ToList());
    }
}
