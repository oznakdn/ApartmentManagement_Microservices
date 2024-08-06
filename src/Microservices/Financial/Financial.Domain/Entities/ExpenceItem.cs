using Shared.Core.Abstracts;

namespace Financial.Domain.Entities;

public class ExpenceItem : Entity
{
    public string ExpenceId { get; private set; }
    public string? UnitId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime LastPaymentDate { get; private set; }
    public bool IsPaid { get; private set; }
    public DateTime? PaymentDate { get; private set; }

    private ExpenceItem() { }
    public ExpenceItem(string expenceId, string? unitId, decimal amount, DateTime lastPaymentDate, bool isPaid, DateTime? paymentDate)
    {
        ExpenceId = expenceId;
        UnitId = unitId;
        Amount = amount;
        LastPaymentDate = lastPaymentDate;
        IsPaid = isPaid;
        PaymentDate = paymentDate;
    }

    public void MarkAsPaid()
    {
        IsPaid = true;
        PaymentDate = DateTime.Now;
    }


}
