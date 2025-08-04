using Shiftly.Domain.Dtos.Emails;

namespace Shiftly.Application.Common.Interfaces.Infrastructure.Services.EmailSender;

public interface ISmtpService
{
    Task SendMail(EmailMessage message);
}