using Apartment.Application.Events.CreatedUnits;
using Apartment.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstracts;
using Shared.Core.Interfaces;

namespace Apartment.Application.Commands.CreateUnits;

public class CreateUnitsHandler : IRequestHandler<CreateUnitsRequest, IResult>
{
    private readonly CommandDbContext _dbContext;
    private readonly IMediator _notification;
    public CreateUnitsHandler(CommandDbContext dbContext, IMediator notification)
    {
        _dbContext = dbContext;
        _notification = notification;
    }

    public async Task<IResult> Handle(CreateUnitsRequest request, CancellationToken cancellationToken)
    {
        var block = await _dbContext.Blocks
            .Include(x => x.Units)
            .SingleOrDefaultAsync(x => x.Id == request.BlockId, cancellationToken);

        if (block is null)
            return Result.Failure(message: "Block not found!");

        if (block.Units.Count == block.TotalUnits)
            return Result.Failure(message: "No units available!");

        var units = new List<Domain.Entities.Unit>();

        for (int i = 0; i < block.TotalUnits; i++)
        {
            var unit = new Domain.Entities.Unit(null, blockId: block.Id, unitNo: i + 1, false);
            units.Add(unit);
            _dbContext.Units.Add(unit);

        }


        block.AddMultipleUnits(units);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result == 0)
            return Result.Failure(message: "Units not created!");

        foreach (var unit in units)
        {
            await _notification.Publish(new CreatedUnitsEvent(unit.Id, unit.BlockId, unit.UnitNo));

        }

        return Result.Success(message: "Units created successfully!");

    }
}
