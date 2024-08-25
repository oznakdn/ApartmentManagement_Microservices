using Report.GraphQL.Models;
using Report.GraphQL.Services;

namespace Report.GraphQL.Schemes;

public class Query
{
    private readonly AggreatorService _aggreatorService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public Query(AggreatorService aggreatorService, IHttpContextAccessor httpContextAccessor)
    {
        _aggreatorService = aggreatorService;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<Site> GetSiteDetailReport(string siteId)
    {
        bool hasToken = _httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var apiKey);

        if (!hasToken)
        {
            return null;
        }

        string token = apiKey.ToString().Split(" ")[1];
        var site = await _aggreatorService.GetSiteDetailReportAsync(token,siteId);
        return site;
    }

}
