namespace Financial.Application.Queries.GetNonPaidExpenceItems;

public record GetNonPaidExpenceItemsResponse(string? UnitId, decimal Amount, string LastPaymentDate);

