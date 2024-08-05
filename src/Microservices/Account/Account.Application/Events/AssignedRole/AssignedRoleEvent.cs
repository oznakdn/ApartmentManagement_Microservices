using Account.Domain.Entities;
using MediatR;

namespace Account.Application.Events.AssignedRole;

public record AssignedRoleEvent(User User, string Role) : INotification;
