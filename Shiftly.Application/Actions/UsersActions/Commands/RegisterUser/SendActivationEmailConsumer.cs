using Marten;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shiftly.Application.Common.Interfaces.Application.Providers;
using Shiftly.Application.Common.Interfaces.Application.Services.Emails;
using Shiftly.Application.Common.Interfaces.Persistence.Repositories;
using Shiftly.Domain.Dtos;
using Shiftly.Domain.Events.User;

namespace Shiftly.Application.Actions.UsersActions.Commands.RegisterUser;

public class SendActivationEmailConsumer(
    IEmailSenderService emailSenderService,
    IEmailContextProvider emailContextProvider,
    IUserRepository userRepository,
    ILogger<SendActivationEmailConsumer> logger)
    : IConsumer<SendActivationEmail>
{
    public async Task Consume(ConsumeContext<SendActivationEmail> context)
    {
        var message = context.Message;

        logger.LogInformation("Processing SendActivationEmail for UserId: {UserId}, Email: {Email}",
            message.UserId, message.Email);

        var expiration = DateTime.UtcNow.AddHours(24);

        var userActivationTokenGenerated = new UserActivationTokenGenerated(message.UserId, expiration);

        await userRepository.AddUserEventAsync(userActivationTokenGenerated);

        var request = new ActivationAccountEmailParametersDto(message.Email, message.UserId,
            userActivationTokenGenerated.ActivationToken, "");

        var emailContext = emailContextProvider.GetContext(request);

        await emailSenderService.SendEmailAsync(
            message.Email,
            "Aktywacja konta",
            emailContext);

        // Zapisz event do Martena
        await userRepository.AddUserEventAsync(new ActivationEmailSent(message.UserId, message.Email, DateTime.UtcNow));
    }
}