using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;
using JobFinder.Web.Models.Company;

namespace JobFinder.Web.Models.JobAds
{
    public class JobListingViewModel : JobAdListingConciseViewModel, IMapFrom<JobAdListingDTO>
    {
        public CompanyBasicDetailsViewModel Company { get; set; }
    }
}
