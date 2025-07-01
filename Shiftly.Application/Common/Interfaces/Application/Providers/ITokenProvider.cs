using Shiftly.Domain.Entities;

namespace Shiftly.Application.Common.Interfaces.Application.Providers;

public interface ITokenProvider
{
    string CreateJwtToken(User user);
    RefreshToken CreateRefreshToken(User user);
}