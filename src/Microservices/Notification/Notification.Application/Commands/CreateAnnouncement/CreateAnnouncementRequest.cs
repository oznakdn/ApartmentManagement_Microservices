using MediatR;

namespace Notification.Application.Commands.CreateAnnouncement;

public record CreateAnnouncementRequest(string ManagerId, string Title, string Content) : IRequest<CreateAnnouncementResponse>;

