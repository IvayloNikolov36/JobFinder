namespace JobFinder.Services.Messages
{
    using JobFinder.Data.Models.ViewsModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DataSender : IDataSender
    {
        private readonly ISubscriptionsService subscriptionsService;
        private readonly IEmailSender emailSender;

        public DataSender(ISubscriptionsService subscriptionsService, IEmailSender emailSender)
        {
            this.subscriptionsService = subscriptionsService;
            this.emailSender = emailSender;
        }

        public async Task SendSubscribersNewJobAds()
        {
            List<CompaniesSubscriptionsData> data = await this.subscriptionsService.GetNewJobAdsForSubscribersAsync();

            foreach (var item in data)
            {
                int companyId = item.CompanyId;
                string[] subscribers = item.Subscribers.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                int[] jobIds = item.JobIds.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                string[] jobPositions = item.JobPositions.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                string[] jobLocations = item.JobLocations.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                //TODO: make table with job ads
                string htmlContent = "html content";
                string subject = $"New job ads from company '{item.CompanyName}'";

                foreach (string userEmail in subscribers)
                {
                    await this.emailSender.SendEmailAsync("jobFinder@abv.bg", "JobFinder", userEmail, subject, htmlContent);
                }
            }
        }
    }
}
