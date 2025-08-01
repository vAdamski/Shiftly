using Shiftly.Domain.Dtos.Emails;

namespace Shiftly.Application.Common.Interfaces.Infrastructure.Services.EmailSender;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessage message);
}