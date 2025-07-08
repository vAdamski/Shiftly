using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shiftly.Application.Actions.UsersActions.Commands.ChangeUserPassword;
using Shiftly.Application.Actions.UsersActions.Commands.RegisterUser;
using Shiftly.Application.Actions.UsersActions.Queries.GetCurrentUserInfo;
using Shiftly.Application.Actions.UsersActions.Queries.GetUserInfo;
using Shiftly.Application.Actions.UsersActions.Queries.LoginUser;
using Shiftly.Application.Actions.UsersActions.Queries.LoginUserWithRefreshToken;

namespace Shiftly.Api.Controllers;

[Route("api/users")]
public class UsersController(ISender sender) : BaseApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await Sender.Send(new GetCurrentUserInfoQuery());
		
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
	
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        var result = await Sender.Send(new GetUserInfoQuery()
        {
            UserId = userId
        });
		
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
	
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess ? Ok() : HandleFailure(result);
    }
	
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
    {
        var result = await Sender.Send(query);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
	
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] LoginUserWithRefreshTokenQuery query)
    {
        var result = await Sender.Send(query);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
    
    [AllowAnonymous]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess ? Ok() : HandleFailure(result);
    }
}