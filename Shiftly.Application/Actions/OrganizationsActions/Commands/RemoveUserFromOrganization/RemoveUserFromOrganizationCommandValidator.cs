using FluentValidation;

namespace Shiftly.Application.Actions.OrganizationsActions.Commands.RemoveUserFromOrganization;

public class RemoveUserFromOrganizationCommandValidator : AbstractValidator<RemoveUserFromOrganizationCommand>
{
	public RemoveUserFromOrganizationCommandValidator()
	{
		RuleFor(x => x.OrganizationId).NotEmpty();
		RuleFor(x => x.UserId).NotEmpty();
	}
}


