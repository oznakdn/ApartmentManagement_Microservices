using Account.Application.Events.UploadedPhoto;
using Account.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Account.Application.Commands.UploadPicture;

public class UploadPictureHandler : IRequestHandler<UploadPictureRequest, IResult>
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

    public async Task<IResult> Handle(UploadPictureRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Failure(errors: validationResult.Errors.Select(x => x.ErrorMessage).ToArray());


        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return Result.Failure(message: "User not found!");


        user.UploadPicture(request.PictureUrl);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return Result.Failure(message: "Failed to upload picture!");


        await _notification.Publish(new UploadedPhotoEvent(user.Id, request.PictureUrl));

        return Result.Success(message: "Picture uploaded successfully.");

    }
}
