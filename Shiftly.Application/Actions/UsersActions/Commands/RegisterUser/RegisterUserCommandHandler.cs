using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Application.Services;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.User;

namespace Shiftly.Application.Actions.UsersActions.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IEventPublisher eventPublisher)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
    
        if (isUserExist is not null)
        {
            return Result.Failure<Guid>(DomainErrors.User.EmailAlreadyExists(request.Email));
        }
    
        var passwordHash = passwordHasher.Hash(request.Password);
    
        var userCreated = new UserCreated(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash);
    
        await userRepository.AppendEventAsync(userCreated, cancellationToken);
    
        var userRegistered = new UserRegistered(
            userCreated.UserId,
            userCreated.FirstName,
            userCreated.Email);
        
        await eventPublisher.PublishAsync(userRegistered, cancellationToken);

        return userCreated.UserId;
    }
}