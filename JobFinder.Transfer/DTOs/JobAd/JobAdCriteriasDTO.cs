namespace JobFinder.Transfer.DTOs.JobAd;

public class JobAdCriteriasDTO
{
    public string Position { get; set; }

    public int CityId { get; set; }

    public int JobCategoryId { get; set; }

    public int JobEngagementId { get; set; }

    public int WorkplaceTypeId { get; set; }

    public bool Intership { get; set; }

    public IEnumerable<int> SoftSkills { get; set; }

    public IEnumerable<int> TechStacks { get; set; }

    public IEnumerable<int> ITAreas { get; set; }
}
