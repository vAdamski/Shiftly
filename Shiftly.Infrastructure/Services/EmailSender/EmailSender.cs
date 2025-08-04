using Microsoft.Extensions.Logging;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services.EmailSender;
using Shiftly.Domain.Dtos.Emails;
using IEmailSender = Shiftly.Application.Common.Interfaces.Infrastructure.Services.EmailSender.IEmailSender;

namespace Shiftly.Infrastructure.Services.EmailSender;

public class EmailSender(ISmtpService smtpService, ILogger<EmailSender> logger) : IEmailSender
{
    public async Task SendEmailAsync(EmailMessage message)
    {
        if (IsDevelopmentEnvironment())
            logger.LogInformation("Email sending is skipped in development environment. Email details: {@EmailMessage}",
                message);
        else
            await smtpService.SendMail(message);
    }

    private bool IsDevelopmentEnvironment()
    {
        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
    }
}