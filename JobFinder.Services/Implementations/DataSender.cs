namespace JobFinder.Services.Messages
{
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DataSender : IDataSender
    {
        private const string JobAdDetailsLink = "https://localhost:44375/JobAds/";
        private const string CompanyDetailsUrl = "https://localhost:44375/company/";

        private readonly ISubscriptionsService subscriptionsService;
        private readonly ICompanySubscriptionsService companySubscriptionsService;
        private readonly IEmailSender emailSender;
        private readonly string sentFromEmail;
        private readonly string sentFromName;

        public DataSender(
            ICompanySubscriptionsService companySubscriptionsService, 
            ISubscriptionsService subscriptionsService, 
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            this.subscriptionsService = subscriptionsService;
            this.companySubscriptionsService = companySubscriptionsService;
            this.emailSender = emailSender;
            this.sentFromEmail = configuration.GetSection("AppAccount:email").Value;
            this.sentFromName = configuration.GetSection("AppAccount:name").Value;
        }

        public async Task SendLatestJobAdsBySubscribedCompanies()
        {
            IEnumerable<CompaniesSubscriptionsDbViewData> data = await this.companySubscriptionsService.GetLatesJobAdsAsync();

            foreach (var item in data)
            {
                int[] jobIds = item.JobIds
                    .Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                string[] subscribers = item.Subscribers
                    .Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string[] jobPositions = item.JobPositions
                    .Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string[] jobLocations = item.JobLocations
                    .Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
                
                
                StringBuilder sb = new();
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
                        .SendEmailAsync(this.sentFromEmail, this.sentFromName, userEmail, subject, sb.ToString());
                }
            }
        }

        public async Task SendLatestJobAdsBySubscribedCategoriesAndLocations()
        {
            IEnumerable<JobAdsSubscriptionsViewModel> data = await this.subscriptionsService
                .GetLatestJobAdsAsync();

            foreach (var item in data)
            {
                string jobCategory = item.JobCategory;
                string location = item.Location;
                string[] subscribers = item.Subscribers;

                StringBuilder sb = new();
                string emailSubject = $"Latest job ads from {jobCategory} category in {location}.";
                
                sb.AppendLine(@$"<div><h4>{emailSubject}</h4><div>");
                sb.AppendLine(@$"<table style=""width: 100%"">");

                int line = 0;
                foreach (LatestJobAdsDbFunctionResult info in item.LatestJobAds)
                {
                    line++;
                    int companyId = info.CompanyId;
                    string companyName = info.CompanyName;

                    string[] positions = info.Positions
                        .Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                    int[] jobAdsIds = info.JobAdsIds
                        .Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                    for (int i = 0; i < positions.Length; i++)
                    {
                        string position = positions[i];

                        sb.AppendLine(@$"<tr style=""border-bottom:1px solid black"">
                                      < td><a href=""{JobAdDetailsLink}{jobAdsIds[i]}"" style=""text-decoration:none"">{position}</a></td>
                                      <td>{location}</td>
                                      <td><a href=""{CompanyDetailsUrl}{companyId}"" style=""text-decoration:none"">{companyName}</a></td>
                                      <td style=""text-align:right;""><img src=""{info.CompanyLogoUrl}"" alt=""CompanyLogo"" width=""90"" height=""90""></td>
                                    </tr>");
                    }
                }

                sb.AppendLine("</table>");

                foreach (string subscriberEmail in subscribers)
                {
                    await this.emailSender.SendEmailAsync(
                        from: this.sentFromEmail,
                        fromName: this.sentFromName,
                        to: subscriberEmail,
                        subject: emailSubject,
                        htmlContent: sb.ToString());
                }
            }
        }

    }
}
