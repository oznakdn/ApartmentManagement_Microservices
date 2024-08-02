using Shared.Core.Abstracts;

namespace Apartment.Domain.Entities;

public class Unit: Entity
{
    public string? ResidentId { get; private set; }
    public string BlockId { get; private set; }
    public int UnitNo { get; private set; }
    public bool IsEmpty => string.IsNullOrWhiteSpace(ResidentId);
    public bool HasCar { get; private set; }

    private Unit() { }

    public Unit(string? residentId, string blockId, int unitNo, bool hasCar)
    {
        ResidentId = residentId;
        BlockId = blockId;
        UnitNo = unitNo;
        HasCar = hasCar;
    }


    public void AssignResident(string residentId)
    {
        ResidentId = residentId;
    }


    public string GetUnitDetail(Block block)
    {
        return $"{block.Id} - {UnitNo}";
    }
}
