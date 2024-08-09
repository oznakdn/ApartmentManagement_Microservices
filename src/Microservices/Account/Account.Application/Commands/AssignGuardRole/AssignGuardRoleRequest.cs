using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.AssignGuardRole;

public record AssignGuardRoleRequest(string UserId) : IRequest<IResult>;
