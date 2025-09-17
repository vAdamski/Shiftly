using System.Security.Cryptography;
using Shiftly.Domain.Events.RefreshToken;

namespace Shiftly.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresTime { get; set; }
}