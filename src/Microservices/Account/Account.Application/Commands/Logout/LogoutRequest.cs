using MediatR;

namespace Account.Application.Commands.Logout;

public record LogoutRequest(string RefreshToken) : IRequest<LogoutResponse>;
