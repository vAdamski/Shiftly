using Shiftly.Application.Actions.UsersActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;

namespace Shiftly.Application.Actions.UsersActions.Queries.GetUserInfo;

public class GetUserInfoQuery : IQuery<UserInfoViewModel>
{
	public Guid UserId { get; set; }
}