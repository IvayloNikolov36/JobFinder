namespace JobFinder.Services.Messages
{
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

        public async Task SendLatestJobAdsBySubscribedCompanies()
        {
            List<CompaniesSubscriptionsData> data = await this.subscriptionsService.GetCompaniesNewJobAdsAsync();

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

        public async Task SendLatestJobAdsBySubscribedCategoriesAndLocations()
        {
            List<JobAdsByCategoryAndLocationViewModel> data = await this.subscriptionsService
                .GetNewJobAdsByCategoryAsync();

            foreach (var item in data)
            {
                //TODO: make table with job ads

                string jobCategory = item.JobCategory;
                string location = item.Location;
                string[] subscribers = item.Subscribers;

                StringBuilder sb = new StringBuilder();
                string emailSubject = $"Latest job ads from {jobCategory} category in {location}.";
                sb.AppendLine(emailSubject);

                foreach (LatestCompanyJobAds info in item.LatestCompanyJobAds)
                {
                    string companyName = info.Name;
                    string[] positions = info.Positions.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string position in positions)
                    {
                        sb.AppendLine($"Company: {companyName}, Job: {position}");
                    }
                }

                foreach (string subscriberEmail in subscribers)
                {
                    await this.emailSender
                        .SendEmailAsync(
                        from: "jobFinder@abv.bg",
                        fromName: "JobFinder",
                        to: subscriberEmail,
                        subject: emailSubject,
                        htmlContent: sb.ToString());
                }
            }
        }

    }
}