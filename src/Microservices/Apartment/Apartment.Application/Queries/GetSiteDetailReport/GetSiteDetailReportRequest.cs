using MediatR;
using Shared.Core.Interfaces;

namespace Apartment.Application.Queries.GetSiteDetailReport;

public record GetSiteDetailReportRequest(string SiteId) : IRequest<IResult<GetSiteDetailReportResponse>>;

