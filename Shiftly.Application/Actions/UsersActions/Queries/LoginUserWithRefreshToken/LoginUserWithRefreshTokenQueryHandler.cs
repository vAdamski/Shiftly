using Shiftly.Application.Actions.UsersActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Application.Providers;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.RefreshToken;

namespace Shiftly.Application.Actions.UsersActions.Queries.LoginUserWithRefreshToken;

public class LoginUserWithRefreshTokenQueryHandler(
    ITokenProvider tokenProvider,
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository)
	: IQueryHandler<LoginUserWithRefreshTokenQuery, LoginUserResponse>
{
	public async Task<Result<LoginUserResponse>> Handle(LoginUserWithRefreshTokenQuery request,
		CancellationToken cancellationToken)
	{
		// Get the refresh token from database
		RefreshToken? refreshToken = await refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);

		if (refreshToken is null)
			return Result.Failure<LoginUserResponse>(DomainErrors.RefreshToken.InvalidRefreshToken);

		if (IsTokenExpired(refreshToken))
			return Result.Failure<LoginUserResponse>(DomainErrors.RefreshToken.ExpiredRefreshToken);

		// Get the user associated with the refresh token
		User? user = await userRepository.GetByIdAsync(refreshToken.UserId, cancellationToken);
		
		if (user is null)
			return Result.Failure<LoginUserResponse>(DomainErrors.User.UserNotFound);

		// Create new access token
		string accessToken = tokenProvider.CreateJwtToken(user);
		
		// Create new refresh token
		var newRefreshTokenDto = tokenProvider.CreateRefreshToken(user);
		
		// Create new refresh token event
		var refreshTokenCreated = new RefreshTokenCreated(
			userId: user.Id,
			refreshToken: newRefreshTokenDto.Token,
			expiresAtInUtc: newRefreshTokenDto.ExpiresAtInUtc
		);
		
		// Store the new refresh token in database
		await refreshTokenRepository.AddAsync(refreshTokenCreated, cancellationToken);
		
		// Delete the old refresh token
		await refreshTokenRepository.DeleteAsync(refreshToken.Id, cancellationToken);

		return Result.Success(new LoginUserResponse()
		{
			Token = accessToken,
			RefreshToken = newRefreshTokenDto.Token,
		});
	}
	
	private bool IsTokenExpired(RefreshToken refreshToken)
	{
		return refreshToken.ExpiresTime < DateTime.UtcNow;
	}
}