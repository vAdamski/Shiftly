using Shiftly.Application.Actions.AuthActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;

namespace Shiftly.Application.Actions.AuthActions.Queries.LoginAccountWithRefreshToken;

public class LoginAccountWithRefreshTokenQuery : IQuery<LoginAccountResponse>
{
	public string RefreshToken { get; set; } = string.Empty;
}
