using Client.WebAdmin.ClientServices;
using Client.WebAdmin.Filters;
using Client.WebAdmin.Models.ApartmentModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.WebAdmin.Pages.Apartment;

[CheckAuthorization]
public class SiteByIdModel(ApartmentService apartmentService) : PageModel
{
    [BindProperty]
    public GetSiteByIdResponse Site { get; set; } = new();


    public async Task OnGetAsync(string siteId)
    {
        Site = await apartmentService.GetSiteById(siteId);
    }
}
