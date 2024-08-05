namespace Financial.Application.Commands.CreateExpence;

public record CreateExpenceResponse(bool Success, string? Message = null, string[]? Errors = null);

