using Apartment.Application.Commands.CreateBlock;
using Apartment.Application.Queries.GetBlockCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace Apartment.Api.Controllers;

[Route("api/apartment/[controller]/[action]")]
[ApiController]
public class BlockController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = RoleConstant.MANAGER)]
    public async Task<IActionResult> Create([FromBody] CreateBlockRequest createBlock, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(createBlock, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }

    [HttpGet]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<IActionResult> GetBlockCount(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetBlockCountRequest(), cancellationToken);
        return Ok(result.Count);
    }

}
