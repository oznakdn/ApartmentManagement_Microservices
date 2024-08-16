using AdminWebApp.Models.ManagerModels;
using Client.WebAdmin.ClientServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.WebAdmin.Pages.Manager;

public class DetailModel(ManagerService managerService) : PageModel
{
    [BindProperty]
    public GetAllManagersResponse Manager { get; set; } = new();

    public async Task OnGetAsync(string managerId)
    {
        var result = await managerService.GetManagerByIdAsync(managerId);

        if(result is not null)
        Manager = result;
    }
}
