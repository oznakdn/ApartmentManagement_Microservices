using Shared.Core.Abstracts;

namespace Report.Domain.Entities;

public class Payment : Entity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SiteId { get; set; }
    public decimal TotalPayment { get; set; }
}
