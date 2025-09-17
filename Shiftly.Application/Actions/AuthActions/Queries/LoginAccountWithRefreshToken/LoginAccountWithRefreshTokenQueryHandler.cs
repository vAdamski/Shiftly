using Shiftly.Application.Actions.AuthActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Application.Providers;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.RefreshToken;

namespace Shiftly.Application.Actions.AuthActions.Queries.LoginAccountWithRefreshToken;

public class LoginAccountWithRefreshTokenQueryHandler(
    ITokenProvider tokenProvider,
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository)
	: IQueryHandler<LoginAccountWithRefreshTokenQuery, LoginAccountResponse>
{
	public async Task<Result<LoginAccountResponse>> Handle(LoginAccountWithRefreshTokenQuery request,
		CancellationToken cancellationToken)
	{
		// Get the refresh token from database
		RefreshToken? refreshToken = await refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);

		if (refreshToken is null)
			return Result.Failure<LoginAccountResponse>(DomainErrors.RefreshToken.InvalidRefreshToken);

		if (IsTokenExpired(refreshToken))
			return Result.Failure<LoginAccountResponse>(DomainErrors.RefreshToken.ExpiredRefreshToken);

		// Get the user associated with the refresh token
		User? user = await userRepository.GetByIdAsync(refreshToken.UserId, cancellationToken);
		
		if (user is null)
			return Result.Failure<LoginAccountResponse>(DomainErrors.User.UserNotFound);

		// Create new access token
		string accessToken = tokenProvider.CreateJwtToken(user);
		
		// Create new refresh token
		var newRefreshTokenDto = tokenProvider.CreateRefreshToken(user);
		
		// Create new refresh token event
		var refreshTokenCreated = new RefreshTokenCreated(
			Id: Guid.CreateVersion7(),
			UserId: user.Id,
			Token: newRefreshTokenDto.Token,
			ExpiresAtInUtc: newRefreshTokenDto.ExpiresAtInUtc
		);
		
		// Store the new refresh token in database
		await refreshTokenRepository.AddAsync(refreshTokenCreated, cancellationToken);
		
		// Delete the old refresh token
		await refreshTokenRepository.DeleteAsync(refreshToken.Id, cancellationToken);

		return Result.Success(new LoginAccountResponse()
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
