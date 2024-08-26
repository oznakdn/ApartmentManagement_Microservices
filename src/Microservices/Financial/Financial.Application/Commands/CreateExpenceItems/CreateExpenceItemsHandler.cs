using Financial.Application.Events.CreatedExpenceItems;
using Financial.Domain.Entities;
using Financial.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Financial.Application.Commands.CreateExpenceItems;

public class CreateExpenceItemsHandler : IRequestHandler<CreateExpenceItemsRequest, IResult>
{
    private readonly WriteDbContext _dbContext;
    private readonly IMediator _eventHandler;
    public CreateExpenceItemsHandler(WriteDbContext dbContext, IMediator eventHandler)
    {
        _dbContext = dbContext;
        _eventHandler = eventHandler;
    }

    public async Task<IResult> Handle(CreateExpenceItemsRequest request, CancellationToken cancellationToken)
    {

        if (request.UnitIds.ToList().Count == 0)
            return Result.Failure(message: "Unit count cannot be zero!");


        var expence = await _dbContext.Expences.SingleOrDefaultAsync(x => x.Id == request.ExpenceId, cancellationToken);

        if (expence is null)
            return Result.Failure(message: "Expence not found!");

        var expenceItems = new List<ExpenceItem>();

        for (int i = 0; i < request.UnitIds.ToList().Count; i++)
        {

            var expenceItem = new ExpenceItem(expenceId: expence.Id,
                unitId: request.UnitIds[i],
                amount: expence.TotalAmount / request.UnitIds.ToList().Count,
                lastPaymentDate: DateTime.Parse(request.LastPaymentDate),
                isPaid: false,
                paymentDate: null);

            expenceItems.Add(expenceItem);
            _dbContext.ExpenceItems.Add(expenceItem);
            await _dbContext.SaveChangesAsync(cancellationToken);
            expence.AddExpenceItem(expenceItem);

        }

        await _eventHandler.Publish(new CreatedExpenceItemsEvent(expenceItems));
        return Result.Success(message: "Expence items created successfully!");
    }
}
