using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Application.Common.Interfaces.Application.Services;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Common;
using Shiftly.Domain.Errors;
using Shiftly.Domain.Events.User;

namespace Shiftly.Application.Actions.UsersActions.Commands.ChangeUserPassword;

public class ChangeUserPasswordCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    : ICommandHandler<ChangeUserPasswordCommand, Guid>
{
    public async Task<Result<Guid>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
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

        UserPasswordChanged userPasswordChangedEvent = new UserPasswordChanged()
        {
            UserId = user.Id,
            PasswordHash = newPasswordHash
        };

        return user.Id;
    }
}