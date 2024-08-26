namespace Apartment.Application.Queries.GetSiteDetailByManagerId;

public record GetSiteDetailByManagerIdResponse(string SiteId, string Name, string Address, List<GetSiteDetailBlocks>? Blocks = null);

public record GetSiteDetailBlocks(string Id, string Name, int TotalUnits, int AvailableUnits, List<GetSiteDetailUnits>? Units = null);

public record GetSiteDetailUnits(string Id, int UnitNo, bool IsEmpty, bool HasCar, string? ResidentId);