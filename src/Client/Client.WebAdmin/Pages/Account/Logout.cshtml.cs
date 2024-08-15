using AspNetCoreHero.ToastNotification.Abstractions;
using Client.WebAdmin.ClientServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.WebAdmin.Pages.Account;

public class LogoutModel(AccountService accountService, INotyfService notyfService) : PageModel
{
    public async Task OnGetAsync()
    {
        await accountService.LogoutAsync();
        notyfService.Success("Logout success");
        Response.Redirect("/Index");
    }
}
