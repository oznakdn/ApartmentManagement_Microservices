using MediatR;

namespace Account.Application.Events.Logined;

public record LoginedEvent(string UserId, string RefreshToken, DateTime RefreshExpire) : INotification;

