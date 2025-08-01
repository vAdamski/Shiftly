using Shiftly.Application.Actions.UsersActions.Commands.RegisterUser;
using Shiftly.Domain.Dtos.Emails;

namespace Shiftly.Application.Common.Interfaces.Application.Builders.Emails;

public interface IEmailBuilder
{
    EmailMessage BuildActivationEmail(ActivationEmailInput input);
}