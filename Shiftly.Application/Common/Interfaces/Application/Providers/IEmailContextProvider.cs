using Shiftly.Domain.Dtos;

namespace Shiftly.Application.Common.Interfaces.Application.Providers;

public interface IEmailContextProvider
{
    string GetContext(EmailBase emailBase);
}