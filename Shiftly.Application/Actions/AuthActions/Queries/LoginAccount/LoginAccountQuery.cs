using Shiftly.Application.Actions.AuthActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;

namespace Shiftly.Application.Actions.AuthActions.Queries.LoginAccount;

public class LoginAccountQuery : IQuery<LoginAccountResponse>
{
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
}
