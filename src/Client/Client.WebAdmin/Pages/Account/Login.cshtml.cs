using AspNetCoreHero.ToastNotification.Abstractions;
using Client.WebAdmin.ClientServices;
using Client.WebAdmin.Models.AccountModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.WebAdmin.Pages.Account;

public class LoginModel(AccountService accountService,INotyfService notyfService) : PageModel
{
    [BindProperty]
    public LoginRequest LoginRequest { get; set; }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            notyfService.Error("Invalid model state");
            return Page();
        }

        var result = await accountService.LoginAsync(LoginRequest);

        if (!result)
        {
            notyfService.Error("Login failed");
            return Page();
        }

        notyfService.Success("Login success");
        return RedirectToPage("/Index");
    }
}
