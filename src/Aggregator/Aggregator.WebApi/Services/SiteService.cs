
using Aggregator.WebApi.Models;
using System.Text.Json;

namespace Aggregator.WebApi.Services;

public class SiteService : ServiceBase
{
    public SiteService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("Site");
    }

    public async Task<GetSiteDetailReportResponse> GetSiteDetailReportAsync(string siteId, string token)
    {

        AddAuthorizationHeader(token);
        string url = $"/api/apartment/site/getSiteDetailReport/{siteId}";
        var responseMessage = await _httpClient.GetAsync(url);

        if (!responseMessage.IsSuccessStatusCode)
            return null;


        var siteCountResponse = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<GetSiteDetailReportResponse>(siteCountResponse);
        return response;


    }
}
