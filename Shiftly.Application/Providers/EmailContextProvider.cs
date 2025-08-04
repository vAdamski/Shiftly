using Shiftly.Application.Common.Interfaces.Application.Builders.Emails;
using Shiftly.Application.Common.Interfaces.Application.Providers;
using Shiftly.Domain.Common;
using Shiftly.Domain.Dtos.Emails;

namespace Shiftly.Application.Providers;

public class EmailContextProvider(
    IActivationEmailBuilder activationBuilder
    // IPasswordResetEmailBuilder passwordResetBuilder
    )
    : IEmailContextProvider
{
    public EmailMessage BuildEmail(EmailBase emailBase)
    {
        return emailBase switch
        {
            ActivationAccountEmailParametersDto activation => activationBuilder.Build(activation),
            // PasswordResetEmailParametersDto reset => passwordResetBuilder.Build(reset),
            _ => throw new NotImplementedException($"No email builder for type: {emailBase.GetType().Name}")
        };
    }
}