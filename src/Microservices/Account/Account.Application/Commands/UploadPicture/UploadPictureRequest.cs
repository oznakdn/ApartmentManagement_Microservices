using MediatR;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.UploadPicture;

public record UploadPictureRequest(string UserId, string PictureUrl) : IRequest<IResult>;
