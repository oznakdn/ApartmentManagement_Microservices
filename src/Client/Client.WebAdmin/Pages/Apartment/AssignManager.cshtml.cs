using AspNetCoreHero.ToastNotification.Abstractions;
using Client.WebAdmin.ClientServices;
using Client.WebAdmin.Models.ApartmentModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.WebAdmin.Pages.Apartment;

public class AssignManagerModel(ManagerService managerService,ApartmentService apartmentService, INotyfService notyfService) : PageModel
{

    [BindProperty]
    public AssignManagerRequest AssignManager { get; set; } = new();

    public SelectList ManagerList { get; set; }

    public async Task OnGetAsync(string siteId)
    {
        AssignManager.SiteId = siteId;
        var managers = await managerService.GetAllManagersAsync();
        var managersWithoutSite = managers.Where(x => x.SiteId == null).ToList();
        ManagerList = new SelectList(managersWithoutSite, "Id", "FullName");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await apartmentService.AssignManagerAsync(AssignManager);

        if (!result)
        {
            notyfService.Error("Failed to assign manager!");
            return Page();
        }

        notyfService.Success("Manager assigned successfully");
        return RedirectToPage("/Apartment/Index");
    }

}
