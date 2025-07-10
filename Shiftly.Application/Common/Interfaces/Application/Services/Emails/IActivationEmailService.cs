using Shiftly.Domain.Dtos;

namespace Shiftly.Application.Common.Interfaces.Application.Services.Emails;

public interface IActivationEmailService
{
    string GenerateActivationEmailBody(ActivationAccountEmailParametersDto dto)
}