using Shiftly.Application.Providers.RefreshToken;
using Shiftly.Domain.Entities;

namespace Shiftly.Application.Common.Interfaces.Application.Providers;

public interface ITokenProvider
{
    string CreateJwtToken(User user);
    RefreshTokenDto CreateRefreshToken(User user);
}