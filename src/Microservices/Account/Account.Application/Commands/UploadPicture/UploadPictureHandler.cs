using Account.Application.Notifications.UploadedPhoto;
using Account.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Account.Application.Commands.UploadPicture;

public class UploadPictureHandler : IRequestHandler<UploadPictureRequest, UploadPictureResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IMediator _notification;
    private readonly IValidator<UploadPictureRequest> _validator;

    public UploadPictureHandler(UserManager<User> userManager, IMediator notification, IValidator<UploadPictureRequest> validator)
    {
        _userManager = userManager;
        _notification = notification;
        _validator = validator;
    }

    public async Task<UploadPictureResponse> Handle(UploadPictureRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new UploadPictureResponse(false, Errors: validationResult.Errors.Select(x => x.ErrorMessage).ToArray());


        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return new UploadPictureResponse(false, "User not found!");


        user.UploadPicture(request.PictureUrl);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return new UploadPictureResponse(false, "Failed to upload picture!");


        await _notification.Publish(new UploadedPhotoEvent(user.Id, request.PictureUrl));

        return new UploadPictureResponse(true, "Picture uploaded successfully.");

    }
}
