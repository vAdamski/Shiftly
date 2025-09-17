using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.User;

public record UserRegisteredConfirmationEmailModel : EventQueue
{
    public Guid UserId { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;

    public override string Serialize()
    {
        return System.Text.Json.JsonSerializer.Serialize(this, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
    }
}