using Account.Infrastructure.Contexts;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Account.Application.Queries.GetProfile;

public class GetProfileHandler : IRequestHandler<GetProfileRequest, GetProfileResponse>
{
    private readonly QueryDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProfileHandler(QueryDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GetProfileResponse> Handle(GetProfileRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        if (user is null)
            return null;

        return _mapper.Map<GetProfileResponse>(user);
    }
}
