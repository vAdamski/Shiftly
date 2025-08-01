namespace Shiftly.Domain.Dtos.Emails;

public record EmailAttachment(string FileName, byte[] Content, string ContentType)
{
    public EmailAttachment(string fileName, byte[] content) : this(fileName, content, "application/octet-stream")
    {
    }
}