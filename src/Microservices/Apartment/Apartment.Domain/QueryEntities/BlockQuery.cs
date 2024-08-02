using Shared.Core.Abstracts;

namespace Apartment.Domain.QueryEntities;

public class BlockQuery : Entity
{
    public string BlockId { get;  set; }
    public string Name { get; set; }
    public string? SiteId { get;  set; }
    public int TotalUnits { get;  set; }
    public int AvailableUnits => TotalUnits - Units.Count;

    private List<UnitQuery> _units = new();
    public IReadOnlyCollection<UnitQuery> Units => _units.AsReadOnly();

    private BlockQuery() { }

    public BlockQuery(string blockId,string name, string siteId, int totalUnits)
    {
        BlockId = blockId;
        Name = name;
        SiteId = siteId;
        TotalUnits = totalUnits;
    }

    public void AddUnit(UnitQuery unit)
    {
        _units.Add(unit);
    }

    public void RemoveUnit(UnitQuery unit)
    {
        _units.Remove(unit);
    }

    public void AddMultipleUnits(List<UnitQuery> units)
    {
        _units.AddRange(units);
    }

    public void RemoveMultipleUnits(List<UnitQuery> units)
    {
        foreach (var item in units)
        {
            _units.Remove(item);
        }
    }
}
