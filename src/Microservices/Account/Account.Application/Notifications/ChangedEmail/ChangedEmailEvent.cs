using MediatR;

namespace Account.Application.Notifications.ChangedEmail;

public record ChangedEmailEvent(string UserId, string NewEmail) : INotification;
