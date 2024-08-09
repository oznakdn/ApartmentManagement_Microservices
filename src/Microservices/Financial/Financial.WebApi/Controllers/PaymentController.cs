﻿using Financial.Application.Commands.CreatePayment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Financial.WebApi.Controllers;

[Route("api/financial/[controller]/[action]")]
[ApiController]
public class PaymentController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result);
    }
}
