using Financial.Application.Commands.CreateExpence;
using Financial.Application.Commands.CreateExpenceItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Constants;

namespace Financial.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ExpenceController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles =RoleConstant.MANAGER)]
    public async Task<IActionResult> Create([FromBody] CreateExpenceRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        if(!result.IsSuccess && result.Errors.Length > 0)
            return BadRequest(result.Errors);

        if(!result.IsSuccess && string.IsNullOrEmpty(result.Message))
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstant.MANAGER)]
    public async Task<IActionResult> CreateItems([FromBody] CreateExpenceItemsRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }
}
