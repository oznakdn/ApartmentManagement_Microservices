using MediatR;

namespace Account.Application.Events.UploadedPhoto;

public record UploadedPhotoEvent(string UserId, string PictureUrl) : INotification;
