using Microsoft.Extensions.Configuration;
using Shiftly.Application.Common.Interfaces.Application.Services.Emails;
using Shiftly.Domain.Dtos;

namespace Shiftly.Application.Services.Emails;

public class ActivationEmailService(IConfiguration config) : IActivationEmailService
{
    private readonly string _frontendBaseUrl = config.GetValue<string>("FrontendBaseUrl")
                                               ?? throw new ArgumentNullException("FrontendBaseUrl is not configured.");

    public string GenerateActivationEmailBody(ActivationAccountEmailParametersDto dto)
    {
        var activationLink = $"{_frontendBaseUrl}/activate?token={dto.ActivationToken}&userId={dto.UserId}";

        return $@"
            <h1>Witaj {dto.FirstName}!</h1>
            <p>Kliknij w poniższy link, aby aktywować swoje konto:</p>
            <a href=""{activationLink}"">{activationLink}</a>
            <p>Jeśli nie zakładałeś konta, zignoruj tę wiadomość.</p>";
    }
}