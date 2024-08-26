using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetUnitById;

public class GetUnitByIdHandler : IRequestHandler<GetUnitByIdRequest, IResult<GetUnitByIdResponse>>
{
    private readonly QueryDbContext _dbContext;
    public GetUnitByIdHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IResult<GetUnitByIdResponse>> Handle(GetUnitByIdRequest request, CancellationToken cancellationToken)
    {
        var unit = await _dbContext.Units
            .AsNoTracking()
            .Where(x => x.Id == request.UnitId)
            .SingleOrDefaultAsync(cancellationToken);


        if(unit is null)
            return Result<GetUnitByIdResponse>.Failure("Unit not found!");

        var result = unit.Map();
        return Result<GetUnitByIdResponse>.Success(result);

    }
}

