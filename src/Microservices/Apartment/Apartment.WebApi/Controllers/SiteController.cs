using Apartment.Application.Commands.AssignEmployeeToSite;
using Apartment.Application.Commands.AssignManagerToSite;
using Apartment.Application.Commands.CreateSite;
using Apartment.Application.Queries.GetAllSite;
using Apartment.Application.Queries.GetSiteById;
using Apartment.Application.Queries.GetSiteByManagerId;
using Apartment.Application.Queries.GetSiteDetailByManagerId;
using Apartment.Application.Queries.GetSiteCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Caching;
using Shared.Core.Constants;
using Apartment.Application.Queries.GetSiteDetailReport;

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

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        await cacheService.RemoveAsync("GetSites");
        return Ok(result.Message);
    }

    [HttpPut]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<IActionResult> AssignManagerToSite([FromBody] AssignManagerToSiteRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Message);


        await cacheService.RemoveAsync("GetManagers");
        await cacheService.RemoveAsync("GetSites");

        return Ok(result.Message);
    }

    [HttpPut]
    [Authorize(Roles = RoleConstant.MANAGER)]
    public async Task<IActionResult> AssignEmployeeToSite([FromBody] AssignEmployeeToSiteRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }



    [HttpGet]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<IActionResult> GetAllSite(CancellationToken cancellationToken)
    {
        var cacheData = await cacheService.GetAllAsync<GetAllSiteResponse>("GetSites");
        if (cacheData is not null)
            return Ok(cacheData);

        var result = await mediator.Send(new GetAllSiteRequest(), cancellationToken);
        if (result is null)
            return NotFound();

        await cacheService.SetAsync("GetSites", result.Values!);

        return Ok(result.Values);
    }


    [HttpGet("{siteId}")]
    [Authorize(Roles = $"{RoleConstant.MANAGER},{RoleConstant.ADMIN}")]
    public async Task<IActionResult> GetSiteById(string siteId, CancellationToken cancellationToken)
    {
        var cacheData = await cacheService.GetAsync<GetSiteByIdRequest>($"GetSiteById-{siteId}");
        if (cacheData is not null)
            return Ok(cacheData);

        var result = await mediator.Send(new GetSiteByIdRequest(siteId), cancellationToken);
        if (result is null)
            return NotFound();

        await cacheService.SetAsync($"GetSiteById-{siteId}", result.Value, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(5),
            SlidingExpiration = TimeSpan.FromSeconds(30)
        });
        return Ok(result.Value);
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


    [HttpGet]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<IActionResult> GetSiteCount(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetSiteCountRequest(), cancellationToken);
        return Ok(result.Count);
    }

    [HttpGet("{siteId}")]
    [Authorize(Roles = $"{RoleConstant.MANAGER},{RoleConstant.ADMIN}")]
    public async Task<IActionResult> GetSiteDetailReport(string siteId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetSiteDetailReportRequest(siteId), cancellationToken);
        return !result.IsSuccess ? NotFound(result.Message) : Ok(result.Value);

    }


}
