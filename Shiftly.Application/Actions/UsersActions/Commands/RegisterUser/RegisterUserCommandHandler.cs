using Marten;
using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Application.Services;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.User;
using Shiftly.Domain.Projections;

namespace Shiftly.Application.Actions.UsersActions.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IEventSender eventSender,
    IDocumentStore documentStore)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.IsExistsAsync(request.Email, cancellationToken))
        {
            return Result.Failure<Guid>(DomainErrors.User.EmailAlreadyExists(request.Email));
        }

        var passwordHash = passwordHasher.Hash(request.Password);

        var userCreated = new UserCreated
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = passwordHash
        };

        await userRepository.AddUserEventAsync(userCreated, cancellationToken);

        var userRegistered = new UserRegisteredConfirmationEmailModel
        {
            UserId = userCreated.UserId,
            FirstName = userCreated.FirstName,
            Email = userCreated.Email
        };

        await eventSender.PublishAsync(userRegistered, cancellationToken);

        return userCreated.UserId;
    }
}