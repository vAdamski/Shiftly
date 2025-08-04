using MailKit.Net.Smtp;
using MimeKit;
using Shiftly.Application.Common.Interfaces.Infrastructure.Services.EmailSender;
using Shiftly.Domain.Common;
using Shiftly.Domain.Dtos.Emails;

namespace Shiftly.Infrastructure.Services.EmailSender;

public class SmtpService(ISmtpConfiguration smtpConfiguration) : ISmtpService
{
    private readonly EmailConfiguration _emailConfiguration = smtpConfiguration.GetEmailSenderConfiguration();

    public async Task SendMail(EmailMessage message)
    {
        MimeMessage email = new MimeMessage();
        email.From.Add(new MailboxAddress(_emailConfiguration.From, _emailConfiguration.From));
        email.To.Add(new MailboxAddress(message.To, message.To));
        email.Subject = message.Subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message.Body
        };

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailConfiguration.Username, _emailConfiguration.Password);
            await client.SendAsync(email);

            await client.DisconnectAsync(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}