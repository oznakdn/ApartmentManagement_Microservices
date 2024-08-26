
using Aggregator.WebApi.Models;
using Aggregator.WebApi.Models.GetNonPaidExpenceItems;
using System.Text.Json;

namespace Aggregator.WebApi.Services;

public class FinancialService : ServiceBase
{
    public FinancialService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("Financial");
    }

    public async Task<GetNonPaidExpenceItemsResponse>GetNonPaidExpenceItemsAsync(string token, string expenceId)
    {
        AddAuthorizationHeader(token);
        string url = $"/api/financial/expence/getnotpaidexpenceitems/{expenceId}";
        HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);

        if (responseMessage.IsSuccessStatusCode)
        {
            string siteCountResponse = await responseMessage.Content.ReadAsStringAsync();
            var response =  JsonSerializer.Deserialize<GetNonPaidExpenceItemsResponse>(siteCountResponse)!;
            return response;
        }

        return null;
    }

    public async Task<GetExpenceReportResponse> GetExpenceReportAsync(string token, string siteId)
    {
        AddAuthorizationHeader(token);
        string url = $"/api/financial/expence/getExpenceReport/{siteId}";

        HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);

        if (responseMessage.IsSuccessStatusCode)
        {
            string siteCountResponse = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<GetExpenceReportResponse>(siteCountResponse)!;
            return response;
        }

        return null;
    }


}
