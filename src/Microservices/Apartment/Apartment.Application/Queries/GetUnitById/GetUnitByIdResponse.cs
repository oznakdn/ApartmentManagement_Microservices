namespace Apartment.Application.Queries.GetUnitById;

public record GetUnitByIdResponse(string Id, int UnitNo, bool IsEmpty, string? ResidentId);

