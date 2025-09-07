using Shiftly.Application.Common.Abstraction.Messaging;

namespace Shiftly.Application.Actions.OrganizationsActions.Commands.RemoveUserFromOrganization;

public class RemoveUserFromOrganizationCommand : ICommand
{
	public Guid OrganizationId { get; set; }
	public Guid UserId { get; set; }
}


