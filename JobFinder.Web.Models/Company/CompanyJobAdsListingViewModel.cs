using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Web.Models.JobAds;
using System.Collections.Generic;

namespace JobFinder.Web.Models.Company
{
    public class CompanyJobAdsListingViewModel : IMapFrom<CompanyJobAdsListingDTO>
    {
        public CompanyBasicDetailsViewModel CompanyDetails { get; set; }

        public IEnumerable<JobAdListingConciseViewModel> Ads { get; set; }
    }
}
