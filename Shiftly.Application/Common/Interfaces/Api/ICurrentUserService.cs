namespace Shiftly.Application.Common.Interfaces.Api;

public interface ICurrentUserService
{
    Guid Id { get; set; }
    string Email { get; set; }
}