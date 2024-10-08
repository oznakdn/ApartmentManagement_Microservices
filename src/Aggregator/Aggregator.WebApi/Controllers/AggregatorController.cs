﻿using Aggregator.WebApi.Models;
using Aggregator.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AggregatorController(ApartmentService apartmentService, FinancialService financialService, SiteService siteService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ApartmentCountsResponse>> GetApartmentCounts()
    {
        bool hasToken = HttpContext.Request.Headers.TryGetValue("Authorization", out var apiKey);
        if(!hasToken)
        {
            return Unauthorized();
        }

        string token = apiKey.ToString().Split(" ")[1];
        var result = await apartmentService.GetApartmentCounts(token);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin,manager")]
    public async Task<ActionResult<ApartmentCountsResponse>> GetNonPaidExpenceItems(string id)
    {
        bool hasToken = HttpContext.Request.Headers.TryGetValue("Authorization", out var apiKey);
        if (!hasToken)
        {
            return Unauthorized();
        }

        string token = apiKey.ToString().Split(" ")[1];
        var result = await financialService.GetNonPaidExpenceItemsAsync(token, id);
        return Ok(result);
    }

    [HttpGet("{siteId}")]
    [Authorize(Roles = "admin,manager")]
    public async Task<ActionResult<ApartmentCountsResponse>> GetSiteDetailReport(string siteId)
    {
        bool hasToken = HttpContext.Request.Headers.TryGetValue("Authorization", out var apiKey);
        if (!hasToken)
        {
            return Unauthorized();
        }

        string token = apiKey.ToString().Split(" ")[1];
        var result = await siteService.GetSiteDetailReportAsync(siteId, token);

        return result is null ? BadRequest() : Ok(result);

    }

    [HttpGet("{siteId}")]
    [Authorize(Roles = "admin,manager")]
    public async Task<ActionResult<ApartmentCountsResponse>> GetExpenceReport(string siteId)
    {
        bool hasToken = HttpContext.Request.Headers.TryGetValue("Authorization", out var apiKey);
        if (!hasToken)
        {
            return Unauthorized();
        }

        string token = apiKey.ToString().Split(" ")[1];
        var result = await financialService.GetExpenceReportAsync(token, siteId);

        return result is null ? BadRequest() : Ok(result);

    }
}
