namespace Aggregator.WebApi.Models.GetNonPaidExpenceItems;

public record GetNonPaidExpenceItemsResponse(string? UnitId, decimal Amount, string LastPaymentDate);

