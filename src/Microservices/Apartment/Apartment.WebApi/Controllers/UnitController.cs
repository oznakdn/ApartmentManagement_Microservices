﻿using Apartment.Application.Commands.AssignResidentToUnit;
using Apartment.Application.Commands.CreateUnits;
using Apartment.Application.Queries.GetUnitCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Constants;

namespace Apartment.Api.Controllers;

[Route("api/apartment/[controller]/[action]")]
[ApiController]
public class UnitController(IMediator mediator) : ControllerBase
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

    [HttpGet]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<IActionResult> GetUnitCount(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUnitCountRequest(), cancellationToken);
        return Ok(result.Count);
    }
}
