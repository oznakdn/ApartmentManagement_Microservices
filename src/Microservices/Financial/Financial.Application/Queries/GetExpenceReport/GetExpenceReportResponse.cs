namespace Financial.Application.Queries.GetExpenceReport;

public record GetExpenceReportResponse(string Title, string Description, decimal Amount, string? CreatedDate);

