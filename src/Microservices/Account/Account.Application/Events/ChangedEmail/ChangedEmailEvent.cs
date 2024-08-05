using MediatR;

namespace Account.Application.Events.ChangedEmail;

public record ChangedEmailEvent(string UserId, string NewEmail) : INotification;
