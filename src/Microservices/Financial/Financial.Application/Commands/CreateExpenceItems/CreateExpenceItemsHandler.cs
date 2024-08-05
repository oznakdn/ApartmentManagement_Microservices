﻿using Financial.Application.Events.CreatedExpenceItems;
using Financial.Domain.Entities;
using Financial.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Financial.Application.Commands.CreateExpenceItems;

public class CreateExpenceItemsHandler : IRequestHandler<CreateExpenceItemsRequest, CreateExpenceItemsResponse>
{
    private readonly CommandDbContext _dbContext;
    private readonly IMediator _eventHandler;
    public CreateExpenceItemsHandler(CommandDbContext dbContext, IMediator eventHandler)
    {
        _dbContext = dbContext;
        _eventHandler = eventHandler;
    }

    public async Task<CreateExpenceItemsResponse> Handle(CreateExpenceItemsRequest request, CancellationToken cancellationToken)
    {

        if (request.UnitIds.Length == 0)
            return new CreateExpenceItemsResponse(false, "Unit count cannot be zero!");


        var expence = await _dbContext.Expences.SingleOrDefaultAsync(x => x.Id == request.ExpenceId, cancellationToken);

        if (expence is null)
            return new CreateExpenceItemsResponse(false, "Expence not found!");

        var expenceItems = new List<ExpenceItem>();

        for (int i = 0; i < request.UnitIds.Length; i++)
        {

            var expenceItem = new ExpenceItem(expenceId: expence.Id,
                unitId: request.UnitIds[i],
                amount: expence.TotalAmount / request.UnitIds.Length,
                lastPaymentDate: request.LastPaymentDate,
                isPaid: false,
                paymentDate: null);

            expenceItems.Add(expenceItem);
           _dbContext.ExpenceItems.Add(expenceItem);
            await _dbContext.SaveChangesAsync(cancellationToken);
            expence.AddExpenceItem(expenceItem);

        }

        await _eventHandler.Publish(new CreatedExpenceItemsEvent(expenceItems));
        return new CreateExpenceItemsResponse(true, "Expence items created successfully!");
    }
}
