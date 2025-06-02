namespace JobFinder.Transfer.DTOs.JobAd;

public class JobAdEditDTO
{
    public string Position { get; set; }

    public string Description { get; set; }

    public int? MinSalary { get; set; }

    public int? MaxSalary { get; set; }

    public int? CurrencyId { get; set; }

    public int JobCategoryId { get; set; }

    public int JobEngagementId { get; set; }

    public int LocationId { get; set; }
}
