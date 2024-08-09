﻿using Financial.Application.Events.CreatedPayment;
using Financial.Domain.Entities;
using Financial.Domain.Enums;
using Financial.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Financial.Application.Commands.CreatePayment;

public class CreatePaymentHandler : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
{
    private readonly CommandDbContext _dbContext;
    private readonly IMediator _eventHandler;
    public CreatePaymentHandler(CommandDbContext dbContext, IMediator eventHandler)
    {
        _dbContext = dbContext;
        _eventHandler = eventHandler;
    }

    public async Task<CreatePaymentResponse> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        var expenceItem = await _dbContext.ExpenceItems.SingleOrDefaultAsync(x => x.Id == request.ExpenceItemId);

        if (expenceItem is null)
            return new CreatePaymentResponse(false, "Expence item not found!");

        if(expenceItem.IsPaid)
            return new CreatePaymentResponse(false, "Expence item is already paid!");

        if(request.PaymentType<1 || request.PaymentType>3)
            return new CreatePaymentResponse(false, "Invalid payment type!");

        var payment = new Payment(expenceItem.Id, DateTime.Now, (PaymentType)request.PaymentType);

        expenceItem.MarkAsPaid();

        await _dbContext.Database.BeginTransactionAsync();

        _dbContext.ExpenceItems.Update(expenceItem);
        _dbContext.Payments.Add(payment);


        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            await _dbContext.Database.RollbackTransactionAsync();
            return new CreatePaymentResponse(false, "Something went wrong!");
        }

        await _dbContext.Database.CommitTransactionAsync();

        await _eventHandler.Publish(new CreatedPaymentEvent(payment.Id, expenceItem.Id, request.PaymentType));

        return new CreatePaymentResponse(true, "Payment created successfully!");
    }
}