using Shiftly.Application.Common.Abstraction.Messaging;

namespace Shiftly.Application.Actions.OrganizationsActions.Commands.AddUserToOrganization;

public class AddUserToOrganizationCommand : ICommand<Guid>
{
	public Guid OrganizationId { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
}


