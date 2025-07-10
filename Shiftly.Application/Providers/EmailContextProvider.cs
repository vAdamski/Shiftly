using Shiftly.Application.Common.Interfaces.Application.Providers;
using Shiftly.Application.Common.Interfaces.Application.Services.Emails;
using Shiftly.Domain.Dtos;

namespace Shiftly.Application.Providers;

public class EmailContextProvider(IActivationEmailService activationEmailService) : IEmailContextProvider
{
    public string GetContext(EmailBase emailBase)
    {
        return emailBase switch
        {
            ActivationAccountEmailParametersDto activationEmail => 
                activationEmailService.GenerateActivationEmailBody(activationEmail),
            _ => throw new NotImplementedException($"No context provider for {emailBase.GetType().Name}")
        };
    }
}