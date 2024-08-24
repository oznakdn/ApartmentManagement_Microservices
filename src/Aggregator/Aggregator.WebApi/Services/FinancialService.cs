
using Aggregator.WebApi.Models.GetNonPaidExpenceItems;
using System.Text.Json;

namespace Aggregator.WebApi.Services;

public class FinancialService : ServiceBase
{
    public FinancialService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public async Task<List<GetNonPaidExpenceItemsResponse>> GetNonPaidExpenceItemsAsync(string token, string expenceId)
    {
       AddAuthorizationHeader(token);
        string url = $"https://localhost:7000/api/financial/expence/getnotpaidexpenceitems/{expenceId}";
        var siteCountResponse = await _httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<List<GetNonPaidExpenceItemsResponse>>(siteCountResponse)!;
    }


}
