namespace JobFinder.Transfer.DTOs.JobAd;

public class JobAdCreateDTO
{
    public string Position { get; set; }

    public string Description { get; set; }

    public int LocationId { get; set; }

    public int JobCategoryId { get; set; }

    public int JobEngagementId { get; set; }

    public int? MinSalary { get; set; }

    public int? MaxSalary { get; set; }

    public int? CurrencyId { get; set; }

    public bool Intership { get; set; }

    public int WorkplaceTypeId { get; set; }

    public IEnumerable<int> SoftSkills { get; set; }

    public IEnumerable<int> ITAreas { get; set; }

    public IEnumerable<int> TechStacks { get; set; }
}
