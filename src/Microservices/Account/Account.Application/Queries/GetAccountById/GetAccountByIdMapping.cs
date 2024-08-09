using Account.Application.Queries.GetAccounts;
using Account.Domain.Entities;

namespace Account.Application.Queries.GetAccountById;

public static class GetAccountByIdMapping
{
    public static GetAccountsResponse Map(this User user)
    {
        var result = new GetAccountsResponse(user.Id,
            user.FullName,
            user.Email,
            user.PhoneNumber,
            user.Address,
            user.Picture,
            user.IsManager,
            user.IsEmployee,
            user.IsResident);

        return result;
    }
}
