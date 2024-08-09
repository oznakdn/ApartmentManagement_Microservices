using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.ChangePassword;

public record ChangePasswordRequest(string UserId, string CurrentPassword, string NewPassword) : IRequest<IResult>;
