using Apartment.Application.Events.AssignedResidentToUnit;
using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;
using Shared.Core.Models;
using Shared.MessagePublising;

namespace Apartment.Application.Commands.AssignResidentToUnit;

public class AssignResidentToUnitHandler : IRequestHandler<AssignResidentToUnitRequest, IResult>
{
    private readonly CommandDbContext _dbContext;
    private readonly IMediator _notification;
    private readonly IMessagePublisher _publisher;
    public AssignResidentToUnitHandler(CommandDbContext dbContext, IMediator notification, IMessagePublisher publisher)
    {
        _dbContext = dbContext;
        _notification = notification;
        _publisher = publisher;
    }

    public async Task<IResult> Handle(AssignResidentToUnitRequest request, CancellationToken cancellationToken)
    {
        var unit = await _dbContext.Units
            .SingleOrDefaultAsync(x => x.Id == request.UnitId, cancellationToken);

        if (unit is null)
            return Result.Failure(message: "Unit not found!");

        unit.AssignResident(request.UserId);

        _dbContext.Units.Update(unit);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result == 0)
            return Result.Failure(message: "Failed to assign resident to unit!");


        await _notification.Publish(new AssignedResidentToUnitEvent(request.UserId, unit.Id));

        await _publisher.PublishAsync<AssignResidentToUnitModel>(queue: QueueConstant.ASSIGN_RESIDENT, messageBody: new AssignResidentToUnitModel
        {
            UnitId = unit.Id,
            ResidentId = request.UserId
        });


        return Result.Success(message: "Resident assigned to unit");
    }
}
