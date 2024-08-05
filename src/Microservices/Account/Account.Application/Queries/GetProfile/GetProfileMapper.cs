using Account.Domain.Entities;
using AutoMapper;

namespace Account.Application.Queries.GetProfile;

public class GetProfileMapper : Profile
{
    public GetProfileMapper()
    {
        CreateMap<GetProfileResponse, User>().ReverseMap();

    }
}
