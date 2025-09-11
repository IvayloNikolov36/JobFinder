using JobFinder.Transfer.DTOs.Company;

namespace JobFinder.Transfer.DTOs.JobAd;

public class JobAdListingDTO : JobAdListingConciseDTO
{
    public CompanyBasicDetailsDTO Company { get; set; }
}
