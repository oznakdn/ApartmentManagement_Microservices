using Shared.Core.Abstracts;

namespace Report.Domain.Entities;

public class Expence : Entity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SiteId { get; set; }
    public decimal TotalExpence { get; set; }
    public bool IsPaid { get; set; }
}
