using Shared.Core.Abstracts;

namespace Apartment.Domain.QueryEntities;

public class UnitQuery : Entity
{
    public string UnitId { get; set; }
    public string? ResidentId { get; set; }
    public string? BlockId { get; set; }
    public int UnitNo { get; set; }
    public bool IsEmpty => string.IsNullOrWhiteSpace(ResidentId);
    public bool HasCar { get; set; }

    public UnitQuery(string? residentId,string unitId, string blockId, int unitNo, bool hasCar)
    {
        ResidentId = residentId;
        UnitId = unitId;
        BlockId = blockId;
        UnitNo = unitNo;
        HasCar = hasCar;
    }
    public void AssignResident(string residentId)
    {
        ResidentId = residentId;
    }


    public string GetUnitDetail(BlockQuery block)
    {
        return $"{block.Id} - {UnitNo}";
    }

}
