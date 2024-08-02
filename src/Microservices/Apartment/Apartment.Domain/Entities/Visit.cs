using Shared.Core.Abstracts;

namespace Apartment.Domain.Entities;

public class Visit : Entity
{
    public string UnitId { get; private set; }
    public string HostName { get; private set; }
    public bool HasCar { get; private set; }
    public string? PlateNumber { get; private set; }
    public DateTime Date { get; private set; }
    public string CreatedBy { get; private set; }
    public string? Notes { get; private set; }

    private Visit() { }

    public Visit(string unitId, string hostName, bool hasCar, string? plateNumber, DateTime date, string createdBy, string? notes)
    {
        UnitId = unitId;
        HostName = hostName;
        HasCar = hasCar;
        PlateNumber = plateNumber;
        Date = date;
        CreatedBy = createdBy;
        Notes = notes;
    }

}
