using Shiftly.Application.Actions.AuthActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Api;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;

namespace Shiftly.Application.Actions.AuthActions.Queries.GetCurrentAccountInfo;

public class GetCurrentAccountInfoQueryHandler(ICurrentUserService currentUserService, IUserRepository userRepository)
	: IQueryHandler<GetCurrentAccountInfoQuery, AccountInfoViewModel>
{
	public async Task<Result<AccountInfoViewModel>> Handle(GetCurrentAccountInfoQuery request,
		CancellationToken cancellationToken)
	{
		var userId = currentUserService.Id;

		var user = await userRepository.GetByIdAsync(userId, cancellationToken);

		if (user == null)
			return Result.Failure<AccountInfoViewModel>(DomainErrors.User.UserNotFound);

		var accountInfo = new AccountInfoViewModel
		{
			Id = user.Id,
			FirstName = user.FirstName,
			LastName = user.LastName,
			Email = user.Email
		};

		return Result.Success(accountInfo);
	}
}
