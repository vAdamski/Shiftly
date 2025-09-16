using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Api;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.Organization;

namespace Shiftly.Application.Actions.OrganizationsActions.Commands.RemoveUserFromOrganization;

public class RemoveUserFromOrganizationCommandHandler(
	ICurrentUserService currentUserService,
	IOrganizationRepository organizationRepository)
	: ICommandHandler<RemoveUserFromOrganizationCommand>
{
	public async Task<Result> Handle(RemoveUserFromOrganizationCommand request, CancellationToken cancellationToken)
	{
		var organization = await organizationRepository.GetByIdAsync(request.OrganizationId, cancellationToken);
		if (organization is null)
		{
			return Result.Failure(DomainErrors.Organization.OrganizationNotFound);
		}

		if (organization.OwnerId != currentUserService.Id)
		{
			return Result.Failure(DomainErrors.Organization.OnlyOwnerCanManageMembers);
		}

		await organizationRepository.AddOrganizationEventAsync(new UserRemovedFromOrganization(request.OrganizationId, request.UserId), cancellationToken);
		return Result.Success();
	}
}



