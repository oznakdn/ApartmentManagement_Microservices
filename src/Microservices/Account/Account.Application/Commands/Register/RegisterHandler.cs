using Account.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Account.Application.Events.CreatedAccount;
using Shared.Core.Interfaces;
using Shared.Core.Abstracts;

namespace Account.Application.Commands.Register;

public class RegisterHandler : IRequestHandler<RegisterRequest, IResult<User>>
{
    private readonly IMediator _notification;
    private readonly UserManager<User> _userManager;
    private readonly IValidator<RegisterRequest> _validator;

    public RegisterHandler(IMediator notification, UserManager<User> userManager, IValidator<RegisterRequest> validator)
    {
        _notification = notification;
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<IResult<User>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result<User>.Failure(errors: validationResult.Errors.Select(x => x.ErrorMessage).ToArray());


        var user = new User(request.FirstName, request.LastName, request.Address, request.PhoneNumber, request.Email, request.Password, request.IsManager, request.IsResident, request.IsEmployee);


        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return Result<User>.Failure();


        await _notification.Publish(new CreatedAccountEvent(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Address,
            user.PhoneNumber!,
            user.Email!,
            user.Picture,
            request.IsManager,
            request.IsEmployee,
            request.IsResident
            ), cancellationToken);


        return Result<User>.Success(value: user, message: "Account created successfully.");
    }
}
