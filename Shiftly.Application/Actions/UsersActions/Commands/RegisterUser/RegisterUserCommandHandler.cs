using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Application;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Errors;

namespace Shiftly.Application.Actions.UsersActions.Commands.RegisterUser;

public class RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
	: ICommandHandler<RegisterUserCommand>
{
	public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);

		if (user is not null)
		{
			return Result.Failure(DomainErrors.User.EmailAlreadyExists(request.Email));
		}
		
		var passwordHash = passwordHasher.Hash(request.Password);

		user = User.Create(
			request.FirstName,
			request.LastName,
			request.Email,
			passwordHash
		);

		await userRepository.AddAsync(user, cancellationToken);

		return Result.Success();
	}
}