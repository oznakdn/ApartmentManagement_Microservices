using MediatR;

namespace Account.Application.Events.RefreshLogined;

public record RefreshLoginedEvent(string RefreshToken, string NewRefreshToken, DateTime RefreshExpire) : INotification;

