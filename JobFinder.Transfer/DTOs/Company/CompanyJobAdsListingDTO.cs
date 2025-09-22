using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.Transfer.DTOs.Company;

public class CompanyJobAdsListingDTO
{
    public CompanyBasicDetailsDTO CompanyDetails { get; set; }

    public IEnumerable<JobAdListingConciseDTO> Ads { get; set; }
}
