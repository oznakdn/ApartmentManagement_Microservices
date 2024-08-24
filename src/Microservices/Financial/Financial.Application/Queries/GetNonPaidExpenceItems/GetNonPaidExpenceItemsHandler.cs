using Financial.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Financial.Application.Queries.GetNonPaidExpenceItems;

public class GetNonPaidExpenceItemsHandler : IRequestHandler<GetNonPaidExpenceItemsRequest, List<GetNonPaidExpenceItemsResponse>>
{
    private readonly QueryDbContext _dbContext;
    public GetNonPaidExpenceItemsHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetNonPaidExpenceItemsResponse>> Handle(GetNonPaidExpenceItemsRequest request, CancellationToken cancellationToken)
    {
        var expenceItems = await _dbContext.ExpenceItems
            .AsNoTracking()
            .Where(x => x.ExpenceId == request.ExpenceId && x.IsPaid == false)
            .ToListAsync(cancellationToken);

        return expenceItems.Map();
    }
}
