using Shiftly.Domain.Common;
using Shiftly.Domain.Events.User;

namespace Shiftly.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public string? ActivationToken { get; set; }
    public DateTime? ActivationTokenExpiration { get; set; }
}