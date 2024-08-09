using Account.Domain.Entities;
using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.Register;

public class RegisterRequest : IRequest<IResult<User>>
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public bool? IsManager { get; set; }
    public bool? IsResident { get; set; }
    public bool? IsEmployee { get; set; }
}
