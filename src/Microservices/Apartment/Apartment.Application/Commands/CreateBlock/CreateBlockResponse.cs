namespace Apartment.Application.Commands.CreateBlock;

public record CreateBlockResponse(bool Success, string? Message=null, string[]? Errors=null);
