namespace Shiftly.Application.Common.Interfaces.Application.Services.Emails;

public interface IEmailSenderService
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}