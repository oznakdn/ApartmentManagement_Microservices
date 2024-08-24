
using Aggregator.WebApi.Models.GetNonPaidExpenceItems;
using System.Text.Json;

namespace Aggregator.WebApi.Services;

public class FinancialService : ServiceBase
{
    public FinancialService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public async Task<GetNonPaidExpenceItemsResponse>GetNonPaidExpenceItemsAsync(string token, string expenceId)
    {
        AddAuthorizationHeader(token);
        string url = $"https://localhost:7000/api/financial/expence/getnotpaidexpenceitems/{expenceId}";
        HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);

        if (responseMessage.IsSuccessStatusCode)
        {
            string siteCountResponse = await responseMessage.Content.ReadAsStringAsync();
            var response =  JsonSerializer.Deserialize<GetNonPaidExpenceItemsResponse>(siteCountResponse)!;
            return response;
        }

        return null;
    }


}
