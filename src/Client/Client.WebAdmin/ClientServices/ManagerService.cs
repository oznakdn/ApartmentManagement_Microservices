using AdminWebApp.Models.ManagerModels;

namespace Client.WebAdmin.ClientServices;

public class ManagerService : ClientServiceBase
{
    public ManagerService(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor) : base(clientFactory, contextAccessor)
    {
    }

    public async Task<IEnumerable<GetAllManagersResponse>> GetAllManagersAsync()
    {
        await base.AddAuthorizationHeader();
        string url = $"{Endpoints.Account.GetManagers}";
        var httpResponse = await _httpClient.GetAsync(url);
        if (httpResponse.IsSuccessStatusCode)
        {
            var response = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<GetAllManagersResponse>>();
            return response!;
        }
        return Enumerable.Empty<GetAllManagersResponse>();
    }

    public async Task<bool> CreateManagerAsync(CreateManagerRequest createManager)
    {
        await base.AddAuthorizationHeader();

        string url = $"{Endpoints.Account.ManagerRegister}";
        var httpResponse = await _httpClient.PostAsJsonAsync<CreateManagerRequest>(url, createManager);
        if (httpResponse.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }
}
