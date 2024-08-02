using MediatR;

namespace Account.Application.Commands.AssignGuardRole;

public record AssignGuardRoleRequest(string UserId) : IRequest<AssignGuardRoleResponse>;
