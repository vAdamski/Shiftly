using Shiftly.Application.Actions.AuthActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;

namespace Shiftly.Application.Actions.AuthActions.Queries.GetAccountInfo;

public class GetAccountInfoQuery : IQuery<AccountInfoViewModel>
{
	public Guid UserId { get; set; }
}
