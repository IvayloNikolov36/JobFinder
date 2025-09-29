using JobFinder.Services.Messaging.Models;
using System.Threading.Tasks;

namespace JobFinder.Services.Messaging;

public interface IEmailSender
{
    Task SendEmailAsync(EmailProps properties);
}
