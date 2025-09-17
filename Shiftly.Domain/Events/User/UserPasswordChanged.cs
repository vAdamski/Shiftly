using Shiftly.Domain.Common;
using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.User;

public record UserPasswordChanged(Guid UserId, string PasswordHash) : UserEvent(UserId);