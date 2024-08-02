using Shared.Core.Abstracts;

namespace Apartment.Domain.QueryEntities;

public class VisitQuery:Entity
{
    public string VisitId { get; set; }
    public string? UnitId { get; set; }
    public string HostName { get; set; }
    public bool HasCar { get; set; }
    public string? PlateNumber { get; set; }
    public DateTime Date { get; set; }
    public string CreatedBy { get; set; }
    public string? Notes { get; set; }

}
