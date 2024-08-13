using Account.Application.Commands.Login;
using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.RefreshLogin;

public class RefreshLoginRequest:IRequest<IResult<LoginResponse>>
{
    public string RefreshToken { get; set; }
}
