using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.User;

public class UserRegisteredConfirmationEmailModel : EventQueue
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public override string Serialize()
    {
        return System.Text.Json.JsonSerializer.Serialize(this, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
    }
}