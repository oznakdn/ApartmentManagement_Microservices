namespace Notification.Application.Commands.CreateAnnouncement;

public record CreateAnnouncementResponse(bool Success, string? Message = null, string[]? Errors = null);

