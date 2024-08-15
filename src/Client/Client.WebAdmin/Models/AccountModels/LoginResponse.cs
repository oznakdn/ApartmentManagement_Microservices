namespace Client.WebAdmin.Models.AccountModels;

public class LoginResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string? Role { get; set; }
    public string AccessToken { get; set; }
    public DateTime AccessExpire { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshExpire { get; set; }
    public string? SiteId { get; set; }
    public string? UnitId { get; set; }
}
