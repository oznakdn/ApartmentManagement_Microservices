﻿using Apartment.Application.Commands.AssignEmployeeToSite;
using Apartment.Application.Commands.AssignManagerToSite;
using Apartment.Application.Commands.CreateSite;
using Apartment.Application.Queries.GetSiteByManagerId;
using Apartment.Application.Queries.GetSiteDetailByManagerId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Caching;
using Shared.Core.Constants;

namespace Apartment.Api.Controllers;

[Route("api/apartment/[controller]/[action]")]
[ApiController]
public class SiteController(IMediator mediator, IDistributedCacheService cacheService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<IActionResult> Create([FromBody] CreateSiteRequest createSite, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(createSite, cancellationToken);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
    }

    [HttpPut]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<IActionResult>AssignManagerToSite([FromBody] AssignManagerToSiteRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }

    [HttpPut]
    [Authorize(Roles = RoleConstant.MANAGER)]
    public async Task<IActionResult> AssignEmployeeToSite([FromBody] AssignEmployeeToSiteRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }


    [HttpGet("{managerId}")]
    [Authorize(Roles = $"{RoleConstant.MANAGER},{RoleConstant.ADMIN}")]
    public async Task<IActionResult> GetSiteByManagerId(string managerId, CancellationToken cancellationToken)
    {
        var cacheData = await cacheService.GetAsync<GetSiteByManagerIdResponse>($"GetSiteByManagerId-{managerId}");
        if (cacheData is not null)
            return Ok(cacheData);

        var result = await mediator.Send(new GetSiteByManagerIdRequest(managerId), cancellationToken);
        if (result is null)
            return NotFound();

        await cacheService.SetAsync($"GetSiteByManagerId-{managerId}", result, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(5),
            SlidingExpiration = TimeSpan.FromSeconds(30)
        });
        return Ok(result);
    }


    [HttpGet("{managerId}")]
    [Authorize(Roles = $"{RoleConstant.MANAGER},{RoleConstant.ADMIN}")]
    public async Task<IActionResult> GetSiteDetailByManagerId(string managerId, CancellationToken cancellationToken)
    {
        var cacheData = await cacheService.GetAsync<GetSiteDetailByManagerIdResponse>($"GetSiteDetailByManagerId-{managerId}");
        if (cacheData is not null)
            return Ok(cacheData);

        var result = await mediator.Send(new GetSiteDetailByManagerIdRequest(managerId), cancellationToken);
        if (result is null)
            return NotFound();


        await cacheService.SetAsync($"GetSiteDetailByManagerId-{managerId}", result, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(5),
            SlidingExpiration = TimeSpan.FromSeconds(30)
        });
        return Ok(result);
    }



}