using Shiftly.Application.Common.Abstraction.Messaging;

namespace Shiftly.Application.Actions.OrganizationsActions.Commands.CreateOrganization;

public class CreateOrganizationCommand : ICommand<Guid>
{
	public string Name { get; set; } = string.Empty;
}


