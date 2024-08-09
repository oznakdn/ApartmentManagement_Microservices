using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.Logout;

public record LogoutRequest(string RefreshToken) : IRequest<IResult>;
