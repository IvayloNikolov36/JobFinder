using System.Collections.Generic;

namespace JobFinder.Services.Messaging.Models;

public class EmailProps
{
    public string SenderEmail { get; set; }

    public string Sender { get; set; }

    public string RecipientEmail { get; set; }

    public string Subject { get; set; }

    public string HtmlContent { get; set; }

    public IEnumerable<EmailAttachment> Attachments { get; set; } = null;
}
