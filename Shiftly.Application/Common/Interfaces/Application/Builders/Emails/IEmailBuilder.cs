using Shiftly.Application.Actions.AuthActions.Commands.RegisterAccount;
using Shiftly.Domain.Dtos.Emails;

namespace Shiftly.Application.Common.Interfaces.Application.Builders.Emails;

public interface IEmailBuilder
{
    EmailMessage BuildActivationEmail(ActivationEmailInput input);
}