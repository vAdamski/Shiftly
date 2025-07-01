using Shiftly.Application.Actions.UsersActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Api;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;

namespace Shiftly.Application.Actions.UsersActions.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoQueryHandler(ICurrentUserService currentUserService, IUserRepository userRepository)
	: IQueryHandler<GetCurrentUserInfoQuery, UserInfoViewModel>
{
	public async Task<Result<UserInfoViewModel>> Handle(GetCurrentUserInfoQuery request,
		CancellationToken cancellationToken)
	{
		var userId = currentUserService.Id;

		var user = await userRepository.GetByIdAsync(userId, cancellationToken);

		if (user == null)
			return Result.Failure<UserInfoViewModel>(DomainErrors.User.UserNotFound);

		var userInfo = new UserInfoViewModel
		{
			Id = user.Id,
			FirstName = user.FirstName,
			LastName = user.LastName,
			Email = user.Email
		};

		return Result.Success(userInfo);
	}
}