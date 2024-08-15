using MediatR;

namespace Account.Application.Events.DeletedManager;

public record DeletedManagerEvent(string UserId) : INotification;

