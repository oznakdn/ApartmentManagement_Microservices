using Apartment.Domain.Entities;

namespace Apartment.Application.Queries.GetUnitById;

public static class GetUnitByIdMapper
{
    public static GetUnitByIdResponse Map(this Unit unit)
    {
        return new GetUnitByIdResponse(unit.Id,unit.UnitNo,unit.IsEmpty,unit.ResidentId);
    }
}
