using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Api;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Events.Organization;

namespace Shiftly.Application.Actions.OrganizationsActions.Commands.CreateOrganization;

public class CreateOrganizationCommandHandler(
	ICurrentUserService currentUserService,
	IOrganizationRepository organizationRepository)
	: ICommandHandler<CreateOrganizationCommand, Guid>
{
	public async Task<Result<Guid>> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
	{
		var organizationId = Guid.NewGuid();
		var ownerId = currentUserService.Id;

		await organizationRepository.AddOrganizationEventAsync(
			new OrganizationCreated(organizationId, request.Name, ownerId), cancellationToken);

		await organizationRepository.AddOrganizationEventAsync(
			new UserAddedToOrganization(organizationId, ownerId), cancellationToken);

		return Result.Success(organizationId);
	}
}


