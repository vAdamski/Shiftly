using Microsoft.EntityFrameworkCore;
using Shiftly.Application.Actions.UsersActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Application.Providers;
using Shiftly.Application.Common.Interfaces.Persistence;
using Shiftly.Domain.Common;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Errors;

namespace Shiftly.Application.Actions.UsersActions.Queries.LoginUserWithRefreshToken;

public class LoginUserWithRefreshTokenQueryHandler(IAppDbContext ctx, ITokenProvider tokenProvider)
	: IQueryHandler<LoginUserWithRefreshTokenQuery, LoginUserResponse>
{
	public async Task<Result<LoginUserResponse>> Handle(LoginUserWithRefreshTokenQuery request,
		CancellationToken cancellationToken)
	{
		RefreshToken? refreshToken = await ctx.RefreshTokens
			.Include(r => r.User)
			.FirstOrDefaultAsync(r => r.Token == request.RefreshToken, cancellationToken);
		
		if (refreshToken is null)
			return Result.Failure<LoginUserResponse>(DomainErrors.RefreshToken.InvalidRefreshToken);
		
		if (IsTokenExpired(refreshToken))
			return Result.Failure<LoginUserResponse>(DomainErrors.RefreshToken.ExpiredRefreshToken);
		
		string accessToken = tokenProvider.CreateJwtToken(refreshToken.User);
		RefreshToken newRefreshToken = tokenProvider.CreateRefreshToken(refreshToken.User);

		refreshToken.Token = newRefreshToken.Token;
		refreshToken.ExpiresTime = newRefreshToken.ExpiresTime;

		await ctx.SaveChangesAsync(cancellationToken);

		return Result.Success(new LoginUserResponse()
		{
			Token = accessToken,
			RefreshToken = refreshToken.Token,
		});
	}
	
	private bool IsTokenExpired(RefreshToken refreshToken)
	{
		return refreshToken.ExpiresTime < DateTime.UtcNow;
	}
}