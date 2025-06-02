using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.Web.Models.JobAds
{
    public class JobListingModel : IMapFrom<JobAdListingDTO>
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string CompanyLogo { get; set; }

        public string CompanyName { get; set; }

        public string Position { get; set; }

        public string PostedOn { get; set; }

        public string JobCategory { get; set; }

        public string JobEngagement { get; set; }

        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }

        public string Currency { get; set; }

        public string Location { get; set; }
    }
}
