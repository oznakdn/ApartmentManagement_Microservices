namespace Apartment.Application.Queries.GetSiteDetailByManagerId;

public record GetSiteDetailByManagerIdResponse(string SiteId, string Name, string Address, List<GetSiteDetailBlocks>? Blocks = null);

public record GetSiteDetailBlocks(string Name, int TotalUnits, int AvailableUnits, List<GetSiteDetailUnits>? Units = null);

public record GetSiteDetailUnits(int UnitNo, bool IsEmpty, bool HasCar, string? ResidentId);