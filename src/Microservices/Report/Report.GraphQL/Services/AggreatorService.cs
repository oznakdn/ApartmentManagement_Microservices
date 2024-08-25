
using Report.GraphQL.Models;

namespace Report.GraphQL.Services;

public class AggreatorService : ServiceBase
{
    public AggreatorService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("Aggregator");
    }

    public async Task<Site> GetSiteDetailReportAsync(string token, string siteId)
    {
        AddAuthorizationHeader(token);
        string url = $"/api/aggregator/getSiteDetailReport/{siteId}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;


        return await response.Content.ReadFromJsonAsync<Site>();

    }
}
