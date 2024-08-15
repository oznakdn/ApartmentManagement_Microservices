using Account.Application.Commands.AssignGuardRole;
using Account.Application.Commands.ChangeEmail;
using Account.Application.Commands.ChangePassword;
using Account.Application.Commands.DeleteManager;
using Account.Application.Commands.Login;
using Account.Application.Commands.Logout;
using Account.Application.Commands.RefreshLogin;
using Account.Application.Commands.Register;
using Account.Application.Commands.UploadPicture;
using Account.Application.Events.AssignedRole;
using Account.Application.Queries.GetAccountById;
using Account.Application.Queries.GetAccounts;
using Account.Application.Queries.GetManagers;
using Account.Application.Queries.GetProfile;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Caching;
using Shared.Core.Constants;

namespace Account.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController(IMediator mediator, IDistributedCacheService cacheService) : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult> AdminRegister([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        request.Password = "Admin123*";
        var result = await mediator.Send(request, cancellationToken);

        if (!result.IsSuccess && result.Errors.Length > 0)
        {
            return BadRequest(result.Errors);
        }

        await mediator.Publish(new AssignedRoleEvent(result.Value!, RoleConstant.ADMIN), cancellationToken);
        return Ok();
    }


    [HttpPost]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<ActionResult> ManagerRegister([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        request.Password = "Manager123*";
        request.IsManager = true;
        var result = await mediator.Send(request, cancellationToken);

        if (!result.IsSuccess && result.Errors.Length > 0)
        {
            return BadRequest(result.Errors);
        }

        await mediator.Publish(new AssignedRoleEvent(result.Value!, RoleConstant.MANAGER), cancellationToken);
        await cacheService.RemoveAsync("Managers");
        return Ok();
    }

    [HttpPost]
    [Authorize(Roles = RoleConstant.MANAGER)]
    public async Task<ActionResult> EmployeeRegister([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        request.Password = "Employee123*";
        request.IsEmployee = true;
        var result = await mediator.Send(request, cancellationToken);

        if (!result.IsSuccess && result.Errors.Length > 0)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }


    [HttpPost]
    [Authorize(Roles = RoleConstant.MANAGER)]
    public async Task<ActionResult> ResidentRegister([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        request.Password = "Resident123*";
        request.IsResident = true;
        var result = await mediator.Send(request, cancellationToken);

        if (!result.IsSuccess && result.Errors.Length > 0)
        {
            return BadRequest(result.Errors);
        }

        await mediator.Publish(new AssignedRoleEvent(result.Value!, RoleConstant.RESIDENT), cancellationToken);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = RoleConstant.MANAGER)]
    public async Task<ActionResult> MakeGuardToEmployee([FromBody] AssignGuardRoleRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var cacheData = await cacheService.GetAsync<LoginResponse>($"Session-{request.Email}");
        if (cacheData is not null)
            return Ok(cacheData);

        var result = await mediator.Send(request, cancellationToken);

        if (!result.IsSuccess && result.Errors.Length > 0)
        {
            return BadRequest(result.Errors);
        }

        if (!result.IsSuccess && result.Message != null)
        {
            return BadRequest(result.Message);
        }

        await cacheService.SetAsync<LoginResponse>($"Session-{request.Email}", result.Value!, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = result.Value!.AccessExpire
        });


        return Ok(result.Value);
    }


    [HttpGet("{refreshToken}")]
    public async Task<IActionResult> RefreshLogin(string refreshToken, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RefreshLoginRequest(refreshToken), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        await cacheService.RemoveAsync($"Session-{result.Value!.Email}");
        return Ok(result.Value);
    }




    [HttpGet("{refreshToken}")]
    public async Task<IActionResult> Logout(string refreshToken, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LogoutRequest(refreshToken), cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest();
        }

        await cacheService.RemoveAsync(result.Value!);
        return Ok();
    }


    [HttpGet("{userId}")]
    [Authorize]
    public async Task<IActionResult> GetProfile(string userId, CancellationToken cancellationToken)
    {
        var cacheData = await cacheService.GetAsync<GetProfileResponse>($"Profile-{userId}");
        if (cacheData is not null)
            return Ok(cacheData);

        var result = await mediator.Send(new GetProfileRequest(userId), cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result.Message);


        await cacheService.SetAsync<GetProfileResponse>($"Profile-{userId}", result.Value!, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5),
            SlidingExpiration = TimeSpan.FromSeconds(30)
        });

        return Ok(result.Value);
    }


    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UploadPhoto([FromBody] UploadPictureRequest uploadPicture, CancellationToken cancellationToken)
    {

        var result = await mediator.Send(uploadPicture, cancellationToken);

        if (!result.IsSuccess && result.Errors.Length > 0)
        {
            return BadRequest(result.Errors);
        }

        if (!result.IsSuccess && result.Message != null)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
    }


    [HttpPut]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePassword, CancellationToken cancellationToken)
    {

        var result = await mediator.Send(changePassword, cancellationToken);

        if (!result.IsSuccess && result.Errors.Length > 0)
        {
            return BadRequest(result.Errors);
        }

        if (!result.IsSuccess && result.Message != null)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest changeEmail, CancellationToken cancellationToken)
    {

        var result = await mediator.Send(changeEmail, cancellationToken);

        if (!result.IsSuccess && result.Errors.Length > 0)
        {
            return BadRequest(result.Errors);
        }

        if (!result.IsSuccess && result.Message != null)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Message);
    }


    [HttpGet]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<IActionResult> GetAccounts(CancellationToken cancellationToken)
    {
        var cacheData = await cacheService.GetAllAsync<GetAccountsResponse>("GetAccounts");
        if (cacheData is not null)
            return Ok(cacheData);

        var result = await mediator.Send(new GetAccountsRequest(), cancellationToken);

        if (result.Values is not null)
        {
            await cacheService.SetListAsync<GetAccountsResponse>($"GetAccounts", result.Values, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5),
                SlidingExpiration = TimeSpan.FromSeconds(30)
            });
        }


        return Ok(result.Values);
    }


    [HttpGet("{id}")]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<IActionResult> GetAccountById(string id, CancellationToken cancellationToken)
    {
        var cacheData = await cacheService.GetAsync<GetAccountsResponse>($"GetAccount-{id}");
        if (cacheData is not null)
            return Ok(cacheData);

        var result = await mediator.Send(new GetAccountByIdRequest(id), cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result.Message);


        await cacheService.SetAsync<GetAccountsResponse>($"GetAccount-{id}", result.Value!, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5),
            SlidingExpiration = TimeSpan.FromSeconds(30)
        });

        return Ok(result);
    }


    [HttpGet]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<IActionResult> GetManagers(CancellationToken cancellationToken)
    {
        var cacheData = await cacheService.GetAllAsync<GetManagersResponse>("Managers");
        if (cacheData is not null)
            return Ok(cacheData);

        var result = await mediator.Send(new GetManagersRequest(), cancellationToken);
        await cacheService.SetListAsync<GetManagersResponse>("Managers", result.Values);
        return Ok(result.Values);
    }


    [HttpDelete("{userId}")]
    [Authorize(Roles = RoleConstant.ADMIN)]
    public async Task<IActionResult>DeleteManager(string userId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteManagerRequest(userId), cancellationToken);
        if(!result.IsSuccess)
            return NotFound(result.Message);

        await cacheService.RemoveAsync("Managers");
        return Ok(result.Message);
    }


}
