using Shiftly.Application.Actions.UsersActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Application;
using Shiftly.Application.Common.Interfaces.Application.Providers;
using Shiftly.Application.Common.Interfaces.Application.Services;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;

namespace Shiftly.Application.Actions.UsersActions.Queries.LoginUser;

public class LoginUserQueryHandler(
	ITokenProvider tokenProvider,
	IUserRepository userRepository,
	IRefreshTokenRepository refreshTokenRepository,
	IPasswordHasher passwordHasher) : IQueryHandler<LoginUserQuery, LoginUserResponse>
{
	public async Task<Result<LoginUserResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);

		if (user is null)
			return Result.Failure<LoginUserResponse>(DomainErrors.User.UserNotFound);

		bool isPasswordValid = passwordHasher.Verify(request.Password, user.PasswordHash);

		if (!isPasswordValid)
			return Result.Failure<LoginUserResponse>(DomainErrors.User.InvalidPassword);
		
		if (!user.IsActive)
			return Result.Failure<LoginUserResponse>(DomainErrors.User.UserNotActivated);

		var token = tokenProvider.CreateJwtToken(user);
		var refreshToken = tokenProvider.CreateRefreshToken(user);
		
		await refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

		return new LoginUserResponse
		{
			Token = token,
			RefreshToken = refreshToken.Token
		};
	}
}