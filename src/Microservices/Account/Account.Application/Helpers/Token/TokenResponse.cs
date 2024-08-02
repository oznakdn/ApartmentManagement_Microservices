namespace Account.Application.Helpers.Token;

public class TokenResponse
{
    public string AccessToken { get; set; }
    public DateTime AccessExpIn { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshExpIn { get; set; }
}
