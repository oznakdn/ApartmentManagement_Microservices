using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Apartment.Domain.Entities;

public class Block : Entity, IAggregateRoot
{
    public string Name { get;private set; }
    public string SiteId { get; private set; }
    public int TotalUnits { get; private set; }
    public int AvailableUnits => TotalUnits - _units.Count;

    private List<Unit> _units = new();
    public IReadOnlyCollection<Unit> Units => _units.AsReadOnly();

    private Block() { }

    public Block(string name, string siteId, int totalUnits)
    {
        Name = name;
        SiteId = siteId;
        TotalUnits = totalUnits;
    }

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);
    }

    public void AddMultipleUnits(List<Unit> units)
    {
        _units.AddRange(units);
    }

    public void RemoveMultipleUnits(List<Unit> units)
    {
        foreach (var item in units)
        {
            _units.Remove(item);
        }
    }
}
