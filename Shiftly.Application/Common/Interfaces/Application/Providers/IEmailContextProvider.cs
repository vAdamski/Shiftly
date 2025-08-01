using Shiftly.Domain.Common;
using Shiftly.Domain.Dtos.Emails;

namespace Shiftly.Application.Common.Interfaces.Application.Providers;

public interface IEmailContextProvider
{
    EmailMessage BuildEmail(EmailBase emailBase);
}