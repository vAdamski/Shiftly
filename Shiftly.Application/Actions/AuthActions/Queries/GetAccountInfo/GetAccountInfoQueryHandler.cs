using Shiftly.Application.Actions.AuthActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;

namespace Shiftly.Application.Actions.AuthActions.Queries.GetAccountInfo;

public class GetAccountInfoQueryHandler(IUserRepository userRepository)
	: IQueryHandler<GetAccountInfoQuery, AccountInfoViewModel>
{
	public async Task<Result<AccountInfoViewModel>> Handle(GetAccountInfoQuery request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

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
