namespace JobFinder.Transfer.DTOs.JobAd;

public class JobAdCategoryDTO
{
    public int JobCategoryId { get; set; }

    public IEnumerable<int> ITAreas { get; set; }

    public IEnumerable<int> TechStacks { get; set; }
}
