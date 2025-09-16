using Shiftly.Application.Common.Abstraction.Messaging;

namespace Shiftly.Application.Actions.AuthActions.Commands.RegisterAccount;

public class RegisterAccountCommand : ICommand<Guid>
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
}
