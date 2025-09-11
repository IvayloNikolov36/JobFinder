using JobFinder.Transfer.DTOs.Company;

namespace JobFinder.Transfer.DTOs;

public class CompanyJobAdsForSubscribersDTO
{
    public CompanyBasicDetailsDTO Company { get; set; }

    public string JobAdIds { get; set; }

    public string Positions { get; set; }

    public string Locations { get; set; }

    public string JobCategories { get; set; }

    public string JobEngagements { get; set; }

    public string Salaries { get; set; }

    public string Subscribers { get; set; }
}
