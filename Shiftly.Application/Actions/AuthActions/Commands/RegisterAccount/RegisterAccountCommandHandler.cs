using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Application.Services;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.User;

namespace Shiftly.Application.Actions.AuthActions.Commands.RegisterAccount;

public class RegisterAccountCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher)
    : ICommandHandler<RegisterAccountCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.IsExistsAsync(request.Email, cancellationToken))
        {
            return Result.Failure<Guid>(DomainErrors.User.EmailAlreadyExists(request.Email));
        }

        var passwordHash = passwordHasher.Hash(request.Password);

        var userCreated = new UserCreated(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash);

        await userRepository.AddUserEventAsync(userCreated, cancellationToken);

        return userCreated.Id;
    }
}
