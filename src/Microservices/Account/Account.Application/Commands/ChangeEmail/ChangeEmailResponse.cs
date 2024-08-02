namespace Account.Application.Commands.ChangeEmail;

public record ChangeEmailResponse(bool Success, string? Message = null, string[]? Errors = null);
