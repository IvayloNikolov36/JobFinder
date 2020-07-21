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
        private readonly ICompanySubscriptionsService companySubscriptionsService;
        private readonly IEmailSender emailSender;

        private const string JobAdDetailsLink = "https://localhost:4200/jobs/";
        private const string CompanyDetailsUrl = "https://localhost:4200/company/";

        public DataSender(
            ICompanySubscriptionsService companySubscriptionsService, 
            ISubscriptionsService subscriptionsService, 
            IEmailSender emailSender)
        {
            this.subscriptionsService = subscriptionsService;
            this.companySubscriptionsService = companySubscriptionsService;
            this.emailSender = emailSender;
        }

        public async Task SendLatestJobAdsBySubscribedCompanies()
        {
            List<CompaniesSubscriptionsData> data = await this.companySubscriptionsService.GetLatesJobAdsAsync();

            foreach (var item in data)
            {
                int[] jobIds = item.JobIds.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                string[] subscribers = item.Subscribers.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                string[] jobPositions = item.JobPositions.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                string[] jobLocations = item.JobLocations.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                
                
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@$"<table style=""width: 100%"">");

                for (int i = 0; i < jobIds.Length; i++)
                {
                    int jobId = jobIds[i];
                    string position = jobPositions[i];
                    string location = jobLocations[i];

                    //string backgroundStyle = string.Empty;
                    //if (i % 2 == 0)
                    //{
                    //    backgroundStyle = @"style=""background-color:#f2f2f2;""";
                    //}

                    sb.AppendLine(@"<hr style=""width: 100%"">");
                    sb.AppendLine(@$"<tr style=""border-bottom:1px solid black"">
                                      <td><a href=""{JobAdDetailsLink}{jobId}"" style=""text-decoration:none"">{position}</a></td>
                                      <td>{location}</td>
                                      <td><a href=""{CompanyDetailsUrl}{item.CompanyId}"" style=""text-decoration:none"">{item.CompanyName}</a></td>
                                      <td style=""text-align:right;""><img src=""{item.CompanyLogo}"" alt=""CompanyLogo"" width=""90"" height=""90""></td>
                                    </tr>");
                }
                sb.AppendLine("</table>");

                string subject = $"New job ads from company '{item.CompanyName}'";
                foreach (string userEmail in subscribers)
                {
                    await this.emailSender
                        .SendEmailAsync("jobFinder@abv.bg", "JobFinder", userEmail, subject, sb.ToString());
                }
            }
        }

        public async Task SendLatestJobAdsBySubscribedCategoriesAndLocations()
        {
            List<JobAdsByCategoryAndLocationViewModel> data = await this.subscriptionsService
                .GetNewJobAdsByCategoryAsync();

            foreach (var item in data)
            {
                string jobCategory = item.JobCategory;
                string location = item.Location;
                string[] subscribers = item.Subscribers;

                StringBuilder sb = new StringBuilder();
                string emailSubject = $"Latest job ads from {jobCategory} category in {location}.";
                
                sb.AppendLine(@$"<div><h4>{emailSubject}</h4><div>");
                sb.AppendLine(@$"<table style=""width: 100%"">");

                int line = 0;
                foreach (LatestCompanyJobAds info in item.LatestCompanyJobAds)
                {
                    line++;
                    int companyId = info.Id;
                    string companyName = info.Name;
                    string[] positions = info.Positions.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
                    int[] jobAdsIds = info.JobAdsIds.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse).ToArray();

                    for (int i = 0; i < positions.Length; i++)
                    {
                        string position = positions[i];

                        sb.AppendLine(@$"<tr style=""border-bottom:1px solid black"">
                                      < td><a href=""{JobAdDetailsLink}{jobAdsIds[i]}"" style=""text-decoration:none"">{position}</a></td>
                                      <td>{location}</td>
                                      <td><a href=""{CompanyDetailsUrl}{companyId}"" style=""text-decoration:none"">{companyName}</a></td>
                                      <td style=""text-align:right;""><img src=""{info.Logo}"" alt=""CompanyLogo"" width=""90"" height=""90""></td>
                                    </tr>");
                    }
                }
                sb.AppendLine("</table>");

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
