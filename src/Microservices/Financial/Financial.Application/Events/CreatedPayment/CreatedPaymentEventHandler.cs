using Financial.Domain.Entities;
using Financial.Domain.Enums;
using Financial.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Financial.Application.Events.CreatedPayment;

public class CreatedPaymentEventHandler : INotificationHandler<CreatedPaymentEvent>
{
    private readonly QueryDbContext _dbContext;

    public CreatedPaymentEventHandler(QueryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreatedPaymentEvent args, CancellationToken cancellationToken)
    {
        var expenceItem = await _dbContext.ExpenceItems.SingleOrDefaultAsync(x => x.Id == args.ExpenceItemId);

        var payment = new Payment(expenceItem.Id, DateTime.Now, (PaymentType)args.PaymentType)
        {
            Id = args.Id
        };

        expenceItem.MarkAsPaid();

        await _dbContext.Database.BeginTransactionAsync();

        _dbContext.ExpenceItems.Update(expenceItem);
        _dbContext.Payments.Add(payment);


        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            await _dbContext.Database.RollbackTransactionAsync();
            return;
        }

        await _dbContext.Database.CommitTransactionAsync();
        return;
    }
}
