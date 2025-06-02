using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;
using JobFinder.Web.Models.Company;

namespace JobFinder.Web.Models.Subscriptions
{
    public class JobAdDetailsForSubscriber : IMapFrom<JobAdDetailsForSubscriberDTO>
    {
        public int Id { get; set; }

        public CompanyBasicViewModel Company { get; set; }

        public string Position { get; set; }

        public string Location { get; set; }

        public string JobCategoryName { get; set; }

        public string JobEngagementName { get; set; }

        public string Salary { get; set; }

        public bool Intership { get; set; }
    }
}
