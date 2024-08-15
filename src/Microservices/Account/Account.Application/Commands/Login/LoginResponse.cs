namespace Account.Application.Commands.Login;



public record LoginResponse(string Id, string Email, string? Role, string AccessToken, DateTime AccessExpire, string RefreshToken, DateTime RefreshExpire, string? SiteId, string? UnitId);
