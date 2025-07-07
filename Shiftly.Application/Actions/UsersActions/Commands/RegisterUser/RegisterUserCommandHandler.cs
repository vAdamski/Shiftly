using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Application.Services;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.User;

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

        var userCreated = new UserCreated(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash);

        await userRepository.AppendEventAsync(userCreated, cancellationToken);

        return Result.Success();
    }
}