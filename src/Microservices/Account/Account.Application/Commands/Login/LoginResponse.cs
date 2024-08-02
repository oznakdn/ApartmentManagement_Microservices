namespace Account.Application.Commands.Login;

public record LoginResponse(bool Success, string? Message = null, string[]? Errors = null, Response? Response = null);

public record Response(string Id, string Email, string? Role, string AccessToken, DateTime AccessExpire, string RefreshToken, DateTime RefreshExpire);
