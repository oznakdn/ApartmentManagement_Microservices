namespace Account.Application.Queries.GetProfile;

public record GetProfileResponse(string Id, string UserId, string FullName, string Email, string PhoneNumber, string Address, string Picture, bool? IsManager, bool? IsEmployee, bool? IsResident);
