using Shiftly.Domain.Dtos.Emails;

namespace Shiftly.Application.Common.Interfaces.Application.Builders.Emails;

public interface IActivationEmailBuilder
{
    EmailMessage Build(ActivationAccountEmailParametersDto dto);
}