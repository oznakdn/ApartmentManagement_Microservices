namespace Account.Application.Commands.AssignGuardRole;

public record AssignGuardRoleResponse(bool Success, string? Message = null, string[]? Errors = null);
