using MediatR;
using Shared.Core.Interfaces;

namespace Financial.Application.Queries.GetExpenceReport;

public record GetExpenceReportRequest(string SiteId) : IRequest<IResult<GetExpenceReportResponse>>;

