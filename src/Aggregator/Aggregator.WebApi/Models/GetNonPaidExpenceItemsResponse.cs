namespace Aggregator.WebApi.Models.GetNonPaidExpenceItems;

public record GetNonPaidExpenceItemsResponse(List<Item> items);

public record Item(string? unitId, decimal amount, string lastPaymentDate);