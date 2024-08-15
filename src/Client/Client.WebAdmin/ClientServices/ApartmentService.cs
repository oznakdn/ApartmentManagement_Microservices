
using AdminWebApp.Models.SiteModels;

namespace Client.WebAdmin.ClientServices;

public class ApartmentService : ClientServiceBase
{
    public ApartmentService(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor) : base(clientFactory, contextAccessor)
    {
    }

    public async Task<bool>CreateSiteAsync(CreateSiteRequest createSite)
    {
       await base.AddAuthorizationHeader();
        var httpResponse = await _httpClient.PostAsJsonAsync("https://localhost:7000/api/site/site/create", createSite);

        if(httpResponse.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<GetAllSiteResponse>>GetAllSiteAsync()
    {
       await base.AddAuthorizationHeader();
        var httpResponse = await _httpClient.GetAsync("https://localhost:7000/api/site/site/getall");
        if (httpResponse.IsSuccessStatusCode)
        {
            var response = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<GetAllSiteResponse>>();
            return response!;
        }
        return Enumerable.Empty<GetAllSiteResponse>();
    }

    public async Task<bool>AssignManagerAsync(AssignManagerRequest assignManager)
    {
       await base.AddAuthorizationHeader();
        var httpResponse = await _httpClient.PostAsJsonAsync("https://localhost:7000/api/site/site/assignmanager", assignManager);
        if (httpResponse.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

    public async Task<GetAllSiteResponse>GetSiteById(string id)
    {
       await base.AddAuthorizationHeader();
        var httpResponse = await _httpClient.GetAsync($"https://localhost:7000/api/site/site/getbyid/{id}");
        if (httpResponse.IsSuccessStatusCode)
        {
            var response = await httpResponse.Content.ReadFromJsonAsync<GetAllSiteResponse>();
            return response!;
        }
        return new GetAllSiteResponse();
    }

    public async Task<bool> UpdateSiteAsync(EditSiteRequest editSite)
    {
       await base.AddAuthorizationHeader();
        var httpResponse = await _httpClient.PutAsJsonAsync<EditSiteRequest>("https://localhost:7000/api/site/site/update", editSite);
        if (httpResponse.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteSiteAsync(string id)
    {
       await base.AddAuthorizationHeader();
        var httpResponse = await _httpClient.DeleteAsync($"https://localhost:7000/api/site/site/delete/{id}");
        if (httpResponse.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

}
