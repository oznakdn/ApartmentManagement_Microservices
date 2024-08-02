using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Commands.UploadPicture
{
    public record UploadPictureResponse(bool Success, string? Message = null, string[]? Errors = null);
}
