using Account.Domain.Entities;
using MediatR;

namespace Account.Application.Notifications.AssignedRole;

public record AssignedRoleEvent(User User, string Role) : INotification;
