using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Commands.CreateAnnouncement;
using Shared.Core.Constants;

namespace Notification.WebSocket.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AnnouncementController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles =RoleConstant.MANAGER)]
    public async Task<IActionResult> Create([FromBody] CreateAnnouncementRequest request)
    {
        var result = await mediator.Send(request);
        if(!result.Success)
            return BadRequest(result.Errors);

        return Ok(result.Message);
    }
}
