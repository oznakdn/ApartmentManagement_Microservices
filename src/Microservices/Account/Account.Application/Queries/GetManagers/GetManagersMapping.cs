using Account.Domain.Entities;

namespace Account.Application.Queries.GetManagers;

public static class GetManagersMapping
{
    public static List<GetManagersResponse> Map(this List<User> users)
    {
        return users.Select(x => new GetManagersResponse(x.Id,x.SiteId, x.FirstName, x.LastName,x.FullName, x.Picture, x.Email, x.PhoneNumber, x.Address)).ToList();
    }
}
