using JobFinder.Data.Models.ViewsModels;
using JobFinder.Services.Messages;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.Subscriptions;
using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace JobFinder.Services.Implementations
{
    public class DataSender : IDataSender
    {
        private const string JobAdDetailsLink = "https://localhost:44375/JobAds/";
        private const string CompanyDetailsUrl = "https://localhost:44375/company/";
        private const char DataSeparator = ';';

        private readonly ISubscriptionsService subscriptionsService;
        private readonly ICompanySubscriptionsService companySubscriptionsService;
        private readonly INomenclatureService nomenclatureService;
        private readonly IEmailSender emailSender;
        private readonly string sentFromEmail;
        private readonly string sentFromName;

        public DataSender(
            ICompanySubscriptionsService companySubscriptionsService,
            ISubscriptionsService subscriptionsService,
            INomenclatureService nomenclatureService,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            this.subscriptionsService = subscriptionsService;
            this.companySubscriptionsService = companySubscriptionsService;
            this.nomenclatureService = nomenclatureService;
            this.emailSender = emailSender;
            this.sentFromEmail = configuration.GetSection("AppAccount:email").Value;
            this.sentFromName = configuration.GetSection("AppAccount:name").Value;
        }

        public async Task SendLatestJobAdsForCompanySubscriptions()
        {
            IEnumerable<CompanyJobAdsForSubscribersViewData> data = await this.companySubscriptionsService
                .GetLatesJobAds();

            foreach (var item in data)
            {
                int[] jobIds = item.JobAdIds
                    .Split([DataSeparator], StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                string[] subscribers = item.Subscribers
                    .Split([DataSeparator], StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string[] jobPositions = item.Positions
                    .Split([DataSeparator], StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string[] jobLocations = item.Locations
                    .Split([DataSeparator], StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                // TODO: parse the data for job engagements, categories, salaries and place it in the html


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

        public async Task SendLatestJobAdsForJobSubscriptions(int recurringTypeId)
        {
            IDictionary<string, List<JobAdsSubscriptionsViewModel>> data = await this.subscriptionsService
                .GetLatestJobAdsAsync(recurringTypeId);

            IEnumerable<BasicViewModel> recurringTypes = await this.nomenclatureService.GetRecurringTypes();
            string recurringType = recurringTypes.FirstOrDefault(rt => rt.Id == recurringTypeId)?.Name;

            foreach (KeyValuePair<string, List<JobAdsSubscriptionsViewModel>> jobAdsBySubscriber in data)
            {
                string subscriber = jobAdsBySubscriber.Key;
                List<JobAdsSubscriptionsViewModel> subscriptionsJobAds = jobAdsBySubscriber.Value;

                string title = $"{recurringType} Job Ads";
                StringBuilder sb = new($"<div><h4>{title}</h4><div>");

                foreach (JobAdsSubscriptionsViewModel item in subscriptionsJobAds)
                {
                    string criterias = this.GetCriteriasText(item);

                    sb.AppendLine(@$"<div><h6>{criterias}</h6><div>");

                    sb.AppendLine(@$"<table style=""width: 100%"">");

                    foreach (JobAdDetailsForSubscriber jobAd in item.JobAds)
                    {
                        sb.AppendLine(this.GetJobAdTableRow(jobAd));
                    }

                    sb.AppendLine("</table>");
                }

                await this.emailSender.SendEmailAsync(
                    from: this.sentFromEmail,
                    fromName: this.sentFromName,
                    to: subscriber,
                    subject: title,
                    htmlContent: sb.ToString());
            }
        }

        private string GetJobAdTableRow(JobAdDetailsForSubscriber jobAd)
        {
            return @$"<tr style=""border-bottom:1px solid black"">
                        <td>
                        <a href=""{JobAdDetailsLink}{jobAd.Id}"" style=""text-decoration:none"">
                            {jobAd.Position}
                        </a>
                        </td>
                        <td>{jobAd.Location}</td>
                        <td>
                        <a href=""{CompanyDetailsUrl}{jobAd.Company.Id}"" style=""text-decoration:none"">
                            {jobAd.Company.Name}
                        </a>
                        </td>
                        <td style=""text-align:right;"">
                        <img src=""{jobAd.Company.Logo}"" alt=""CompanyLogo"" width=""90"" height=""90"">
                        </td>
                     </tr>";
        }

        private string GetCriteriasText(JobAdsSubscriptionsViewModel item)
        {
            string content = string.Empty;

            if (item.JobCategory != null)
            {
                content += $"Job Category: {item.JobCategory}";
            }

            if (item.JobEngagement != null)
            {
                content += $", Job Engagement: {item.JobEngagement}";
            }

            if (item.Location != null)
            {
                content += $", Location: {item.Location}";
            }

            if (item.SearchTerm != null)
            {
                content += $", For Jobs with Titles that contains: {item.SearchTerm}";
            }

            if (item.Intership)
            {
                content += " Only Intership";
            }

            if (item.SpecifiedSalary)
            {
                content += "; with specified salary range";
            }

            return content.TrimStart([',', ' ']);
        }
    }
}
