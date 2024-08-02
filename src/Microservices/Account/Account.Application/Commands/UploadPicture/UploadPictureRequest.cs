using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Commands.UploadPicture
{
    public record UploadPictureRequest(string UserId, string PictureUrl) : IRequest<UploadPictureResponse>;
}
