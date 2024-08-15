using AdminWebApp.Models.ManagerModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Client.WebAdmin.ClientServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.WebAdmin.Pages.Manager;

public class IndexModel(ManagerService managerService, INotyfService notyfService) : PageModel
{

    [BindProperty]
    public CreateManagerRequest CreateManager { get; set; }

    [BindProperty]
    public List<GetAllManagersResponse> Managers { get; set; } = new();


    public async Task OnGetAsync()
    {
        var result = await managerService.GetAllManagersAsync();
        Managers = result.ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await managerService.CreateManagerAsync(CreateManager);
        if (!result)
        {
            notyfService.Error("Failed to create manager");
            return Page();
        }
        notyfService.Success("Manager created successfully");
        return RedirectToPage("/Manager/Index");
    }
}
