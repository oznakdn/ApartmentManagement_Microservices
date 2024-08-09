using Account.Domain.Entities;

namespace Account.Application.Queries.GetAccounts;

public static class GetAccountsMapping
{
    public static List<GetAccountsResponse> Map(this List<User> users)
    {

        return users.Select(x => new GetAccountsResponse(
        x.Id,
        x.FullName,
        x.Email,
        x.PhoneNumber,
        x.Address,
        x.Picture,
        x.IsManager,
        x.IsEmployee,
        x.IsResident
        )).ToList();
    }
}
