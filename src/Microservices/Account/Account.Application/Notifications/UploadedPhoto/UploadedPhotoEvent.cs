using MediatR;

namespace Account.Application.Notifications.UploadedPhoto;

public record UploadedPhotoEvent(string UserId, string PictureUrl) : INotification;
