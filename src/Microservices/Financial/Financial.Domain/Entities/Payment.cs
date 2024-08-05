using Financial.Domain.Enums;
using Shared.Core.Abstracts;

namespace Financial.Domain.Entities;

public class Payment : Entity
{
    public string ExpenceItemId { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public PaymentType PaymentType { get; private set; }

    private Payment() { }

    public Payment(string expenceItemId, DateTime paymentDate, PaymentType paymentType)
    {
        ExpenceItemId = expenceItemId;
        PaymentDate = paymentDate;
        PaymentType = paymentType;
    }

    public void SetPayment()
    {
        PaymentDate = DateTime.Now;
    }

}
