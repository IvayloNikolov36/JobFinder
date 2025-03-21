﻿namespace JobFinder.Services.Messages
{
    using Microsoft.Extensions.Configuration;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SendGridEmailSender : IEmailSender
    {
        private readonly SendGridClient client;

        public SendGridEmailSender(IConfiguration configuration)
        {
            string apiKey = configuration.GetSection("SendGrid:apiKey").Value;
            this.client = new SendGridClient(apiKey);            
        }

        public async Task SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            if (string.IsNullOrWhiteSpace(subject) && string.IsNullOrWhiteSpace(htmlContent))
            {
                throw new ArgumentException("Subject and message should be provided.");
            }

            EmailAddress fromAddress = new EmailAddress(from, fromName);
            EmailAddress toAddress = new EmailAddress(to);
            SendGridMessage message = MailHelper.CreateSingleEmail(fromAddress, toAddress, subject, null, htmlContent);

            if (attachments?.Any() == true)
            {
                foreach (EmailAttachment attachment in attachments)
                {
                    message.AddAttachment(attachment.FileName,
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
