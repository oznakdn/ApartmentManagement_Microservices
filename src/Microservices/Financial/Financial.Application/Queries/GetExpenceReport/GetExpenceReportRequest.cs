using MediatR;
using Shared.Core.Interfaces;

namespace Financial.Application.Queries.GetExpenceReport;

public record GetExpenceReportRequest(string Title, string Description, decimal Amount) : IRequest<IResult<GetExpenceReportResponse>>;

