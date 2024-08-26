using Apartment.Application.Commands.AssignResidentToUnit;
using Apartment.Application.Commands.CreateUnits;
using Apartment.Application.Queries.GetUnitById;
using Apartment.Application.Queries.GetUnitCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Caching;
using Shared.Core.Constants;

namespace Apartment.Api.Controllers;

[Route("api/apartment/[controller]/[action]")]
[ApiController]
public class UnitController(IMediator mediator, IDistributedCacheService cacheService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = RoleConstant.MANAGER)]
    public async Task<IActionResult> Create([FromBody] CreateUnitsRequest createUnits, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(createUnits, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }

    [HttpPut]
    [Authorize(Roles = RoleConstant.MANAGER)]
    public async Task<IActionResult> AssignResidentToUnit([FromBody] AssignResidentToUnitRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }

    [HttpGet("{unitId}")]
    [Authorize(Roles = RoleConstant.MANAGER)]
    public async Task<IActionResult> GetUnitById(string unitId, CancellationToken cancellationToken)
    {
        var cacheData = await cacheService.GetAsync<GetUnitByIdRequest>($"GetUnitById-{unitId}");
        if (cacheData is not null)
            return Ok(cacheData);

        var result = await mediator.Send(new GetUnitByIdRequest(unitId), cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        await cacheService.SetAsync($"GetUnitById-{unitId}", result.Value, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(5),
            SlidingExpiration = TimeSpan.FromSeconds(30)
        });


        return Ok(result.Value);
    }

    [HttpGet]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<IActionResult> GetUnitCount(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUnitCountRequest(), cancellationToken);
        return Ok(result.Count);
    }
}
