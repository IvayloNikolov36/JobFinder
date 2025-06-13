using JobFinder.Transfer.DTOs.Company;

namespace JobFinder.Transfer.DTOs.JobAd;

public class JobAdDetailsForSubscriberDTO
{
    public int Id { get; set; }

    public CompanyBasicDTO Company { get; set; }

    public string Position { get; set; }

    public string Location { get; set; }

    public string JobCategoryName { get; set; }

    public string JobEngagementName { get; set; }

    public int? MinSalary { get; set; }

    public int? MaxSalary { get; set; }

    public string CurrencyName { get; set; }

    public bool Intership { get; set; }
}
