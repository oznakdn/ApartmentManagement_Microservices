using Account.Infrastructure.Contexts;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Account.Application.Queries.GetProfile;

public class GetProfileHandler : IRequestHandler<GetProfileRequest, IResult<GetProfileResponse>>
{
    private readonly QueryDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProfileHandler(QueryDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IResult<GetProfileResponse>> Handle(GetProfileRequest request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user is null)
            return Result<GetProfileResponse>.Failure(message: "User not found!");

        var result = user.Map();
        return Result<GetProfileResponse>.Success(result);
    }
}
