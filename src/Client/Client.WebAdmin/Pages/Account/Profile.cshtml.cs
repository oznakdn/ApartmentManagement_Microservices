using AspNetCoreHero.ToastNotification.Abstractions;
using Client.WebAdmin.ClientServices;
using Client.WebAdmin.Filters;
using Client.WebAdmin.Models.AccountModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Client.WebAdmin.Pages.Account;


[CheckAuthorization]
public class ProfileModel(AccountService accountService, INotyfService notyfService) : PageModel
{
    [BindProperty]
    public GetProfileResponse Profile { get; set; } = new();

    [BindProperty]
    public UploadPictureRequest UploadPicture { get; set; } = new();

    [BindProperty]
    public ChangePasswordRequest ChangePassword { get; set; } = new();

    public async Task OnGetAsync()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        UploadPicture.UserId = id;
        Profile = await accountService.GetProfileAsync(id!);

    }

    //TODO: Method account service tarafinda yazilacak
    //public async Task<IActionResult> OnPostEditProfile()
    //{
    //    var result = await accountService.UpdateProfileAsync(Profile);
    //    if (result)
    //    {
    //        notyfService.Success("Update success");
    //        return RedirectToPage("/Account/Profile");
    //    }

    //    notyfService.Error("Update failed");
    //    return Page();
    //}

    public async Task<IActionResult> OnPostUploadPicture(IFormFile file)
    {

        if (file is null)
        {
            notyfService.Error("Please select a file");
            return RedirectToPage("/Account/Profile");
        }

        UploadPicture.PictureUrl = $"{UploadPicture.UserId}-{file.FileName}";
        var result = await accountService.UploadPictureAsync(UploadPicture);
        if (result)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProfilePictures", UploadPicture.PictureUrl);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            notyfService.Success("Update success");
            return RedirectToPage("/Account/Profile");
        }

        notyfService.Error("Update failed");
        return Page();
    }

    public async Task<IActionResult> OnPostChangePassword()
    {

        if (ChangePassword.NewPassword != ChangePassword.ConfirmNewPassword)
        {
            notyfService.Error("Confirm password not match");
            return RedirectToPage("/Account/Profile");
        }

        if (string.IsNullOrWhiteSpace(ChangePassword.CurrentPassword) || string.IsNullOrWhiteSpace(ChangePassword.NewPassword) || string.IsNullOrWhiteSpace(ChangePassword.ConfirmNewPassword))
        {
            notyfService.Error("Please enter all field");
            return RedirectToPage("/Account/Profile");
        }

        string email = User.FindFirst(ClaimTypes.Email)!.Value;
        ChangePassword.CurrentPassword = email;
        var result = await accountService.ChangePasswordAsync(ChangePassword);
        if (result)
        {
            notyfService.Success("Password changed success");
            return RedirectToPage("/Account/Login");
        }

        notyfService.Error("Change failed");
        return RedirectToPage("/Account/Profile");
    }

}
