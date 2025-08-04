using Shiftly.Application.Common.Interfaces.Application.Handlers;
using Shiftly.Application.Common.Interfaces.Application.Providers;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services.EmailSender;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Dtos.Emails;
using Shiftly.Domain.Events.User;

namespace Shiftly.Application.Actions.UsersActions.Commands.RegisterUser;

public class SendActivationEmailHandler(
    IEmailContextProvider emailContextProvider,
    IEmailSender emailSender,
    IUserRepository userRepository)
    : ISendActivationEmailHandler
{
    public async Task HandleAsync(SendActivationEmail message, CancellationToken cancellationToken)
    {
        var expiration = DateTime.UtcNow.AddHours(24);

        var userActivationTokenGenerated = new UserActivationTokenGenerated(message.UserId, expiration);
        await userRepository.AddUserEventAsync(userActivationTokenGenerated, cancellationToken);

        var emailMessage = emailContextProvider.BuildEmail(new ActivationAccountEmailParametersDto(
            message.Email,
            message.UserId,
            userActivationTokenGenerated.ActivationToken.ToString(),
            message.FirstName));

        await emailSender.SendEmailAsync(emailMessage);
        
        var activationEmailSent = new ActivationEmailSent(message.UserId, message.Email, DateTime.UtcNow);
        await userRepository.AddUserEventAsync(activationEmailSent, cancellationToken);
    }
}