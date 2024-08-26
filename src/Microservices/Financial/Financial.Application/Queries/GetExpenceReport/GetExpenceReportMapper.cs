using Financial.Domain.Entities;

namespace Financial.Application.Queries.GetExpenceReport;

public static class GetExpenceReportMapper
{
    public static GetExpenceReportResponse Map (this Expence expence)
    {
        return new GetExpenceReportResponse(expence.Title, expence.Description, expence.TotalAmount, expence.CreatedDate.ToString());
    }
}
