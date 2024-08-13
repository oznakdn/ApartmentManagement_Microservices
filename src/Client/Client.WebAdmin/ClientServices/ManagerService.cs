using AdminWebApp.Models.ManagerModels;

namespace Client.WebAdmin.ClientServices;

public class ManagerService : ClientServiceBase
{
    public ManagerService(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor) : base(clientFactory, contextAccessor)
    {
    }

    public async Task<IEnumerable<GetAllManagersResponse>>GetAllManagersAsync()
    {
        base.AddAuthorizationHeader();
        var httpResponse = await _httpClient.GetAsync("https://localhost:7000/api/auth/manager/getmanagers");
        if (httpResponse.IsSuccessStatusCode)
        {
            var response = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<GetAllManagersResponse>>();
            return response!;
        }
        return Enumerable.Empty<GetAllManagersResponse>();
    }

    public async Task<bool>CreateManagerAsync(CreateManagerRequest createManager)
    {
        base.AddAuthorizationHeader();
        var httpResponse = await _httpClient.PostAsJsonAsync<CreateManagerRequest>("https://localhost:7000/api/auth/manager/register",createManager);
        if (httpResponse.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }
}
