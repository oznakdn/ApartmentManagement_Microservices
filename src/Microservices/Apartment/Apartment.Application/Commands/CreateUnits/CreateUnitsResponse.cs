namespace Apartment.Application.Commands.CreateUnits;

public record CreateUnitsResponse(bool Success, string? Message = null, string[]? Errors = null);

