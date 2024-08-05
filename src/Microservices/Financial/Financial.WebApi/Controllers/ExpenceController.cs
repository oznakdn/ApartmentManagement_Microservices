using Financial.Application.Commands.CreateExpence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Financial.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ExpenceController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExpenceRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        if(!result.Success && result.Errors.Length > 0)
            return BadRequest(result.Errors);

        if(!result.Success && string.IsNullOrEmpty(result.Message))
            return BadRequest(result.Message);

        return Ok(result);
    }
}
