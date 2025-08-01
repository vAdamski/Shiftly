using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Shiftly.Application.Common.Interfaces.Application.Services.Emails;

namespace Shiftly.Infrastructure.Services.EmailSender;

public class EmailSenderSender(ILogger<EmailSenderSender> logger) : IEmailSenderService
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            // Here you would implement the logic to send the email.
            // For example, using an SMTP client or an email service provider API.
            logger.LogInformation("Sending email to {Email} with subject: {Subject}", email, subject);
            // Simulate sending email
            await Task.Delay(1000);
            logger.LogInformation("Email sent successfully to {Email}", email);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error sending email to {Email}", email);
            throw; // Re-throw the exception after logging it
        }
    }
}