using Shiftly.Application.Actions.UsersActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;

namespace Shiftly.Application.Actions.UsersActions.Queries.LoginUser;

public class LoginUserQuery : IQuery<LoginUserResponse>
{
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
}