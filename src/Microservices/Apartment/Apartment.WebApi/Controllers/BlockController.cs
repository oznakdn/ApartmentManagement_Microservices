using Apartment.Application.Commands.CreateBlock;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Constants;

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

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }

}
