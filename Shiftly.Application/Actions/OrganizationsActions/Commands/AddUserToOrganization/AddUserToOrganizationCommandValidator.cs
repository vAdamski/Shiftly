using FluentValidation;

namespace Shiftly.Application.Actions.OrganizationsActions.Commands.AddUserToOrganization;

public class AddUserToOrganizationCommandValidator : AbstractValidator<AddUserToOrganizationCommand>
{
	public AddUserToOrganizationCommandValidator()
	{
		RuleFor(x => x.OrganizationId).NotEmpty();
		RuleFor(x => x.FirstName).NotEmpty().MaximumLength(128);
		RuleFor(x => x.LastName).NotEmpty().MaximumLength(128);
		RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
	}
}


