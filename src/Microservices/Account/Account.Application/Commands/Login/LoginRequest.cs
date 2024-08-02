using MediatR;

namespace Account.Application.Commands.Login;

public record LoginRequest(string Email, string Password) : IRequest<LoginResponse>;
