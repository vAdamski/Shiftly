using Shiftly.Application.Common.Interfaces.Api;

namespace Shiftly.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    public Guid Id { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000001");
    public string Email { get; set; } = "test@test.com";
}