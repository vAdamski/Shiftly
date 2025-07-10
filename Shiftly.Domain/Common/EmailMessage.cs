namespace Shiftly.Domain.Common;

public class EmailMessage
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public EmailMessage(string toEmail, string subject, string body)
    {
        ToEmail = toEmail;
        Subject = subject;
        Body = body;
    }
}