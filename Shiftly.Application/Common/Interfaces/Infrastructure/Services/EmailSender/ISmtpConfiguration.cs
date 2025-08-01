using Shiftly.Domain.Common;

namespace Shiftly.Application.Common.Interfaces.Infrastructure.Services.EmailSender;

public interface ISmtpConfiguration
{
    EmailConfiguration GetEmailSenderConfiguration();
}