using Shiftly.Domain.Common;

namespace Shiftly.Application.Common.Interfaces.Infrastructure.Services.EmailSender;

public interface IEmailSenderConfiguration
{
    EmailConfiguration GetEmailSenderConfiguration();
}