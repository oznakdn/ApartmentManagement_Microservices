namespace Account.Application.Queries.GetManagers;

public record GetManagersResponse(string Id,string? SiteId, string FirstName, string LastName, string? Picture, string Email, string PhoneNumber, string Address);

