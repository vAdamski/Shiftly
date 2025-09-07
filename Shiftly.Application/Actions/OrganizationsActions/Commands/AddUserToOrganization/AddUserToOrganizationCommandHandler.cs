using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Api;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Application.Common.Interfaces.Application.Services;
using Shiftly.Application.Helpers;
using Shiftly.Domain.Common;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.Organization;
using Shiftly.Domain.Events.User;

namespace Shiftly.Application.Actions.OrganizationsActions.Commands.AddUserToOrganization;

public class AddUserToOrganizationCommandHandler(
	ICurrentUserService currentUserService,
	IOrganizationRepository organizationRepository,
	IUserRepository userRepository,
	IPasswordHasher passwordHasher)
	: ICommandHandler<AddUserToOrganizationCommand, Guid>
{
	public async Task<Result<Guid>> Handle(AddUserToOrganizationCommand request, CancellationToken cancellationToken)
	{
		var organization = await organizationRepository.GetByIdAsync(request.OrganizationId, cancellationToken);
		if (organization is null)
		{
			return Result.Failure<Guid>(DomainErrors.Organization.OrganizationNotFound);
		}

		if (organization.OwnerId != currentUserService.Id)
		{
			return Result.Failure<Guid>(DomainErrors.Organization.OnlyOwnerCanManageMembers);
		}

		if (await userRepository.IsExistsAsync(request.Email, cancellationToken))
		{
			return Result.Failure<Guid>(DomainErrors.User.EmailAlreadyExists(request.Email));
		}

		var generatedPassword = StrongPasswordGenerator.Generate();
		var passwordHash = passwordHasher.Hash(generatedPassword);

		var userCreated = new UserCreated
		{
			FirstName = request.FirstName,
			LastName = request.LastName,
			Email = request.Email,
			PasswordHash = passwordHash
		};

		await userRepository.AddUserEventAsync(userCreated, cancellationToken);
		await organizationRepository.AddOrganizationEventAsync(new UserAddedToOrganization(request.OrganizationId, userCreated.UserId), cancellationToken);

		return Result.Success(userCreated.UserId);
	}
}


