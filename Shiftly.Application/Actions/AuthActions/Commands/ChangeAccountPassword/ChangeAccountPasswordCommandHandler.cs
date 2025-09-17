using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Application.Services;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.User;

namespace Shiftly.Application.Actions.AuthActions.Commands.ChangeAccountPassword;

public class ChangeAccountPasswordCommandHandler(
    IUserRepository userRepository, 
    IPasswordHasher passwordHasher)
    : ICommandHandler<ChangeAccountPasswordCommand, Guid>
{
    public async Task<Result<Guid>> Handle(ChangeAccountPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.UserEmail, cancellationToken);
        
        if (user == null)
        {
            return Result.Failure<Guid>(DomainErrors.User.UserNotFound);
        }
        
        bool isPasswordValid = passwordHasher.Verify(request.OldPassword, user.PasswordHash);
        
        if (!isPasswordValid)
        {
            return Result.Failure<Guid>(DomainErrors.User.InvalidPassword);
        }

        var newPasswordHash = passwordHasher.Hash(request.NewPassword);

        var userPasswordChangedEvent = new UserPasswordChanged(user.Id, newPasswordHash);

        await userRepository.AddUserEventAsync(userPasswordChangedEvent, cancellationToken);

        return user.Id;
    }
}
