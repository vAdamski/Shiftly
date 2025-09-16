using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.User;

namespace Shiftly.Application.Actions.AuthActions.Commands.ActivateAccount;

public class ActivateAccountCommandHandler(IUserRepository userRepository) : ICommandHandler<ActivateAccountCommand>
{
    public async Task<Result> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
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
