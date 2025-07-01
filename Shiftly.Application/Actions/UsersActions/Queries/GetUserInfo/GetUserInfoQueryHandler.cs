using Shiftly.Application.Actions.UsersActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;

namespace Shiftly.Application.Actions.UsersActions.Queries.GetUserInfo;

public class GetUserInfoQueryHandler(IUserRepository userRepository)
	: IQueryHandler<GetUserInfoQuery, UserInfoViewModel>
{
	public async Task<Result<UserInfoViewModel>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

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