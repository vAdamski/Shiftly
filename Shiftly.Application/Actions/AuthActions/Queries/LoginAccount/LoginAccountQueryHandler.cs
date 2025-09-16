using Shiftly.Application.Actions.AuthActions.Queries.Shared;
using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Application.Providers;
using Shiftly.Application.Common.Interfaces.Application.Services;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.RefreshToken;
using Shiftly.Domain.Events.User;

namespace Shiftly.Application.Actions.AuthActions.Queries.LoginAccount;

public class LoginAccountQueryHandler(
    ITokenProvider tokenProvider,
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IPasswordHasher passwordHasher) : IQueryHandler<LoginAccountQuery, LoginAccountResponse>
{
    public async Task<Result<LoginAccountResponse>> Handle(LoginAccountQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
            return Result.Failure<LoginAccountResponse>(DomainErrors.User.UserNotFound);

        bool isPasswordValid = passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid)
            return Result.Failure<LoginAccountResponse>(DomainErrors.User.InvalidPassword);

        if (!user.IsActive)
            return Result.Failure<LoginAccountResponse>(DomainErrors.User.UserNotActivated);

        var token = tokenProvider.CreateJwtToken(user);
        var refreshTokenDto = tokenProvider.CreateRefreshToken(user);

        var refreshTokenCreatedEvent = new RefreshTokenCreated(
            refreshTokenDto.UserId,
            refreshTokenDto.Token,
            refreshTokenDto.ExpiresAtInUtc);

        await refreshTokenRepository.AddAsync(refreshTokenCreatedEvent, cancellationToken);

        var userLoggedInEvent = new UserLoggedIn(user.Id, token, refreshTokenDto.Token);
        await userRepository.AddUserEventAsync(userLoggedInEvent, cancellationToken);

        return new LoginAccountResponse
        {
            Token = token,
            RefreshToken = refreshTokenDto.Token
        };
    }
}
