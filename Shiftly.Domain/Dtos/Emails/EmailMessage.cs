using Shiftly.Domain.Common;

namespace Shiftly.Domain.Dtos.Emails;

public record EmailMessage(string To, string Subject, string Body, List<EmailAttachment>? Attachments = null);