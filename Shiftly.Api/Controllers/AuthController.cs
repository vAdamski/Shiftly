using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shiftly.Application.Actions.AuthActions.Commands.ActivateAccount;
using Shiftly.Application.Actions.AuthActions.Commands.ChangeAccountPassword;
using Shiftly.Application.Actions.AuthActions.Commands.RegisterAccount;
using Shiftly.Application.Actions.AuthActions.Queries.GetCurrentAccountInfo;
using Shiftly.Application.Actions.AuthActions.Queries.GetAccountInfo;
using Shiftly.Application.Actions.AuthActions.Queries.LoginAccount;
using Shiftly.Application.Actions.AuthActions.Queries.LoginAccountWithRefreshToken;

namespace Shiftly.Api.Controllers;

[Route("api/auth")]
public class AuthController(ISender sender) : BaseApiController(sender)
{
    [HttpGet("users/current-user")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await Sender.Send(new GetCurrentAccountInfoQuery());
		
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
	
    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        var result = await Sender.Send(new GetAccountInfoQuery()
        {
            UserId = userId
        });
		
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
	
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterAccountCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [AllowAnonymous]
    [HttpPost("activate")]
    public async Task<IActionResult> Activate([FromQuery] ActivateAccountCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess ? Ok() : HandleFailure(result);
    }
	
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginAccountQuery query)
    {
        var result = await Sender.Send(query);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
	
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] LoginAccountWithRefreshTokenQuery query)
    {
        var result = await Sender.Send(query);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
    
    [AllowAnonymous]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangeAccountPasswordCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess ? Ok() : HandleFailure(result);
    }
}