using Account.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Commands.Register
{
    public record RegisterResponse(bool Success, string[]? Errors = null, User? User = null);
}
