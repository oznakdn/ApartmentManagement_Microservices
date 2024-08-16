using AdminWebApp.Models.ManagerModels;
using AdminWebApp.Models.SiteModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Client.WebAdmin.ClientServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.WebAdmin.Pages.Apartment;

public class IndexModel(ApartmentService apartmentService,ManagerService managerService, INotyfService notyfService) : PageModel
{
    [BindProperty]
    public IEnumerable<GetAllSiteResponse> Sites { get; set; } = new List<GetAllSiteResponse>();

    [BindProperty]
    public CreateManagerRequest CreateManager { get; set; } = new();

    [BindProperty]
    public CreateSiteRequest CreateSite { get; set; }

    public SelectList ManagerList { get; set; }

   
    public async Task OnGetAsync()
    {
        Sites = await apartmentService.GetAllSiteAsync();

        var managers = await managerService.GetAllManagersAsync();
        var managersWithoutSite = managers.Where(x => x.SiteId == null).ToList();
        ManagerList = new SelectList(managersWithoutSite, "Id", "FullName");
    }

    public async Task<IActionResult> OnPostAsync()
    {

        var result = await apartmentService.CreateSiteAsync(CreateSite);
        if (!result)
        {
            notyfService.Error("Failed to create site!");
            return Page();
        }

        notyfService.Success("Site created successfully");
        return RedirectToPage("/Apartment/Index");

    }

}
