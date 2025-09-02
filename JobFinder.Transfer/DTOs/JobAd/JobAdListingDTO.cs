namespace JobFinder.Transfer.DTOs.JobAd;

public class JobAdListingDTO : JobAdListingConciseDTO
{
    // TODO: use a nested DTO instead of these 3 props

    public int CompanyId { get; set; }

    public string CompanyLogo { get; set; }

    public string CompanyName { get; set; }
}
