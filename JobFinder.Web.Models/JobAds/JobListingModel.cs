using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.Web.Models.JobAds
{
    public class JobListingModel : JobAdListingConciseViewModel, IMapFrom<JobAdListingDTO>
    {
        // TODO: use a DTO for these three props

        public int CompanyId { get; set; }

        public string CompanyLogo { get; set; }

        public string CompanyName { get; set; }

    }
}
