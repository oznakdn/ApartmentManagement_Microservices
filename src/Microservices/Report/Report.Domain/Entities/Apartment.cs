using Shared.Core.Abstracts;

namespace Report.Domain.Entities;

public class Apartment : Entity
{
    public string SiteId { get; set; }
    public int Blocks { get; set; }
    public int TotalUnits { get; set; }
    public int EmptyUnits { get; set; }
    public int FullUnits { get; set; }
}
