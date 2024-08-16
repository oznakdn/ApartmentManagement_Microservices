using Account.Application.Queries.GetManagers;
using Account.Domain.Entities;

namespace Account.Application.Queries.GetManagerById;

public static class GetManagerByIdMapper
{
    public static GetManagersResponse Map(this User user)
    {
        return new GetManagersResponse(user.Id, user.SiteId, user.FirstName, user.LastName, user.FullName, user.Picture, user.Email, user.PhoneNumber, user.Address);
    }
}
