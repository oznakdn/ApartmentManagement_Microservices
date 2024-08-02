using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Commands.UploadPicture
{
    public class UploadPictureValidator : AbstractValidator<UploadPictureRequest>
    {
        public UploadPictureValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.PictureUrl).NotEmpty().NotNull();
        }
    }
}
