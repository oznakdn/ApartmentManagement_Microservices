using Account.Application.Commands.Login;
using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.RefreshLogin;

public record RefreshLoginRequest(string RefreshToken) :IRequest<IResult<LoginResponse>>;

