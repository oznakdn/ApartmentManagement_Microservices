using Financial.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Financial.Application.Queries.GetExpenceReport;

public class GetExpenceReportHandler : IRequestHandler<GetExpenceReportRequest, IResult<GetExpenceReportResponse>>
{
    private readonly ReadDbContext _dbContext;
    public GetExpenceReportHandler(ReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IResult<GetExpenceReportResponse>> Handle(GetExpenceReportRequest request, CancellationToken cancellationToken)
    {
        var expence = await _dbContext.Expences
            .AsNoTracking()
            .Where(x => x.SiteId == request.SiteId)
            .SingleOrDefaultAsync(cancellationToken);

        if (expence is null)
            return Result<GetExpenceReportResponse>.Failure("Expence not found!");

        var result = expence.Map();

        return Result<GetExpenceReportResponse>.Success(value: result);
    }
}
