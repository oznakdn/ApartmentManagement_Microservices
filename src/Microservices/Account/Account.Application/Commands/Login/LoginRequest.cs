using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.Login;

public record LoginRequest(string Email, string Password) : IRequest<IResult<LoginResponse>>;
