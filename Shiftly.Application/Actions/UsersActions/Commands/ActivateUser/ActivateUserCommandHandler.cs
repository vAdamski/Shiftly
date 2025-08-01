using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.User;

namespace Shiftly.Application.Actions.UsersActions.Commands.ActivateUser;

public class ActivateUserCommandHandler(IUserRepository userRepository) : ICommandHandler<ActivateUserCommand>
{
    public async Task<Result> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        
        if (user is null)
            return Result.Failure(DomainErrors.User.UserNotFound);
        
        if (user.IsActive)
            return Result.Failure(DomainErrors.User.UserAlreadyActivated);

        if (user.ActivationToken != request.ActivationToken)
            return Result.Failure(DomainErrors.User.InvalidActivationToken);
        
        var userActivated = new UserActivated(request.UserId, DateTime.UtcNow);
        await userRepository.AddUserEventAsync(userActivated, cancellationToken);

        return Result.Success();
    }
}