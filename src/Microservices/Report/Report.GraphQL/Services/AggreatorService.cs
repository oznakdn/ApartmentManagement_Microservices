
namespace Report.GraphQL.Services;

public class AggreatorService : ServiceBase
{
    public AggreatorService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("Apartment");
    }
}
