using Microsoft.Extensions.Configuration;
using Shiftly.Application.Common.Interfaces.Application.Builders.Emails;
using Shiftly.Domain.Dtos.Emails;

namespace Shiftly.Application.Builders.Emails;

public class ActivationEmailBuilder(IConfiguration config) : IActivationEmailBuilder
{
    private readonly string _frontendBaseUrl = config.GetValue<string>("ApplicationSettings:FrontendBaseUrl")
                                               ?? throw new ArgumentNullException("FrontendBaseUrl");

    public EmailMessage Build(ActivationAccountEmailParametersDto dto)
    {
        var link = $"{_frontendBaseUrl}/activate?token={dto.ActivationToken}&userId={dto.UserId}";

        var body = $"""
                    <h1>Witaj {dto.FirstName}!</h1>
                    <p>Kliknij w link, aby aktywować konto:</p>
                    <a href="{link}">{link}</a>
                    <p>Jeśli to nie Ty, zignoruj wiadomość.</p>
                    """;

        return new EmailMessage(dto.Email, "Aktywacja konta", body);
    }
}