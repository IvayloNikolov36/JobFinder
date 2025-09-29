using JobFinder.Services.Messaging.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Services.Messaging
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly SendGridClient client;

        public SendGridEmailSender(IConfiguration configuration)
        {
            string apiKey = configuration.GetSection("SendGrid:apiKey").Value;
            this.client = new SendGridClient(apiKey);
        }

        public async Task SendEmailAsync(EmailProps properties)
        {
            if (string.IsNullOrWhiteSpace(properties.Subject)
                && string.IsNullOrWhiteSpace(properties.HtmlContent))
            {
                throw new ArgumentException("Subject and message should be provided.");
            }

            EmailAddress fromAddress = new EmailAddress(properties.SenderEmail, properties.Sender);
            EmailAddress toAddress = new EmailAddress(properties.RecipientEmail);

            SendGridMessage message = MailHelper.CreateSingleEmail(
                fromAddress,
                toAddress,
                properties.Subject,
                plainTextContent: null,
                properties.HtmlContent);

            if (properties.Attachments != null && properties.Attachments.Any())
            {
                foreach (EmailAttachment attachment in properties.Attachments)
                {
                    message.AddAttachment(
                        attachment.FileName,
                        Convert.ToBase64String(attachment.Content),
                        attachment.MimeType);
                }
            }

            try
            {
                Response response = await this.client.SendEmailAsync(message);
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(await response.Body.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
