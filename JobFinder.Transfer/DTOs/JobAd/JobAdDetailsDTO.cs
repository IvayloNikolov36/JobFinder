namespace JobFinder.Transfer.DTOs.JobAd;

public class JobAdDetailsDTO
{
    public int Id { get; set; }

    public string Position { get; set; }

    public string Desription { get; set; }

    public string Location { get; set; }

    public string PostedOn { get; set; }

    public string JobEngagement { get; set; }

    public string CompanyLogo { get; set; }

    public string CompanyName { get; set; }

    public int? MinSalary { get; set; }

    public int? MaxSalary { get; set; }

    public string Currency { get; set; }
}
