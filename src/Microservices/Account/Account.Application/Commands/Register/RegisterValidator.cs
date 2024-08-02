using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Commands.Register
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().Length(3, 30);
            RuleFor(x => x.LastName).NotEmpty().NotNull().Length(3, 20);
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Address).NotEmpty().NotNull().Length(10, 50);
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().Length(13, 13);
            //RuleFor(x => x.Password).NotEmpty().NotNull().Length(6, 11).Must(x => PasswordValidation(x));

        }

        public bool PasswordValidation(string password)
        {
            bool result = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsUpper(password[i]) ||
                    char.IsLower(password[i]) ||
                    char.IsDigit(password[i]) ||
                    char.IsLetter(password[i]) ||
                    char.IsSymbol(password[i]))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }

            return result;
        }
    }
}
