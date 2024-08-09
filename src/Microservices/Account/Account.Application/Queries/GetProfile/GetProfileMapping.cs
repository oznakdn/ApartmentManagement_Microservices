using Account.Domain.Entities;

namespace Account.Application.Queries.GetProfile;

public static class GetProfileMapping
{
    public static GetProfileResponse Map(this User user)
    {
        return new GetProfileResponse(user.Id, user.FullName, user.Email, user.PhoneNumber, user.Address, user.Picture, user.IsManager, user.IsEmployee, user.IsResident);
    }
}
