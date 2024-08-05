using Account.Application.Events.ChangedEmail;
using Account.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Account.Application.Commands.ChangeEmail;

public class ChangeEmailHandler : IRequestHandler<ChangeEmailRequest, ChangeEmailResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IMediator _notification;
    private readonly IValidator<ChangeEmailRequest> _validator;

    public ChangeEmailHandler(UserManager<User> userManager, IMediator notification, IValidator<ChangeEmailRequest> validator)
    {
        _userManager = userManager;
        _notification = notification;
        _validator = validator;
    }

    public async Task<ChangeEmailResponse> Handle(ChangeEmailRequest request, CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(request);

        if (!validation.IsValid)
            return new ChangeEmailResponse(false, Errors: validation.Errors.Select(x => x.ErrorMessage).ToArray());

        var user = await _userManager.FindByEmailAsync(request.CurrentEmail);

        if (user is null)
            return new ChangeEmailResponse(false, Message: "User not found!");

        user.ChangeEmail(request.NewEmail);
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return new ChangeEmailResponse(false, Message: "Email change failed!");

        await _notification.Publish(new ChangedEmailEvent(user.Id, request.NewEmail));

        return new ChangeEmailResponse(true, Message: "Email changed successfully", null);
    }
}
