namespace Account.Application.Queries.GetProfile;

public record GetProfileResponse(string Id, string FirstName, string LastName, string? Picture, string Email, string PhoneNumber, string Address, bool? IsManager, bool? IsEmployee, bool? IsResident);
