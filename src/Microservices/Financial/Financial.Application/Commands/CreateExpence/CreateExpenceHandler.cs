using Financial.Application.Events.CreatedExpence;
using Financial.Domain.Entities;
using Financial.Infrastructure.Contexts;
using FluentValidation;
using MediatR;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Financial.Application.Commands.CreateExpence;

public class CreateExpenceHandler : IRequestHandler<CreateExpenceRequest, IResult>
{
    private readonly CommandDbContext _dbContext;
    private readonly IValidator<CreateExpenceRequest> _validator;
    private readonly IMediator _eventHandler;
    public CreateExpenceHandler(CommandDbContext dbContext, IValidator<CreateExpenceRequest> validator, IMediator eventHandler)
    {
        _dbContext = dbContext;
        _validator = validator;
        _eventHandler = eventHandler;
    }

    public async Task<IResult> Handle(CreateExpenceRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Failure(errors: validationResult.Errors.Select(x => x.ErrorMessage).ToArray());


        var expence = new Expence(request.Title, request.Description, request.TotalAmount);
        _dbContext.Expences.Add(expence);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result == 0)
            return Result.Failure(message: "Failed to create expence!");


        await _eventHandler.Publish(new CreatedExpenceEvent(expence.Id, expence.Title, expence.Description, expence.TotalAmount));

        return Result.Success(message: "Expence created successfully.");


    }
}
