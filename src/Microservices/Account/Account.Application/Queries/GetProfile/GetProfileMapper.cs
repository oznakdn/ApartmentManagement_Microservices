using Account.Domain.QueryEntities;
using AutoMapper;

namespace Account.Application.Queries.GetProfile;

public class GetProfileMapper : Profile
{
    public GetProfileMapper()
    {
        CreateMap<GetProfileResponse, UserQuery>().ReverseMap();

    }
}
