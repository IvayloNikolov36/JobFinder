namespace JobFinder.Transfer.DTOs.Company;

public class CompanyProfileDataDTO
{
    public int? LogoImageId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public int ActiveAdsCount { get; set; }

    public int[] NewApplications { get; set; }
}
