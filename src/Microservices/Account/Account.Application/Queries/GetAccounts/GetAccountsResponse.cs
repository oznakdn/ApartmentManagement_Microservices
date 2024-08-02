namespace Account.Application.Queries.GetAccounts;

public record GetAccountsResponse(string Id, string UserId, string FullName, string Email, string PhoneNumber, string Address, string Picture, bool? IsManager, bool? IsEmployee, bool? IsResident);
