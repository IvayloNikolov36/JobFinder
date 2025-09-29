namespace JobFinder.Services.Messaging.Models;

public class EmailAttachment
{
    public byte[] Content { get; set; }

    public string FileName { get; set; }

    public string MimeType { get; set; }
}
