namespace Account.Application.Commands.ChangePassword;

public record ChangePasswordResponse(bool Success, string? Message = null, string[]? Errors = null);
