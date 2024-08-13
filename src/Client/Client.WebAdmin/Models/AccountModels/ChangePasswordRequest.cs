using System.ComponentModel.DataAnnotations;

namespace Client.WebAdmin.Models.AccountModels;

public class ChangePasswordRequest
{
    public string UserId { get; set; }

    [Required]
    public string CurrentPassword { get; set; }

    [Required]
    public string NewPassword { get; set; }

    [Required]
    [Compare(nameof(NewPassword))]
    public string ConfirmNewPassword { get; set; }

}
