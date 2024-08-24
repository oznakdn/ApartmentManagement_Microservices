using Financial.Domain.Entities;

namespace Financial.Application.Queries.GetNonPaidExpenceItems;

public static class GetNonPaidExpenceItemsMapper
{
    public static List<GetNonPaidExpenceItemsResponse> Map(this List<ExpenceItem> expenceItems)
    {
        var result = expenceItems.Select(x => new GetNonPaidExpenceItemsResponse(x.UnitId, x.Amount, x.LastPaymentDate.ToShortDateString())).ToList();

        return result;
    }
}
