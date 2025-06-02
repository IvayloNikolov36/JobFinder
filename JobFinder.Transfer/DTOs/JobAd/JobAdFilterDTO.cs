namespace JobFinder.Transfer.DTOs.JobAd;

public class JobAdFilterDTO
{
    public int Page { get; set; }

    public int Items { get; set; }

    public string SearchText { get; set; }

    public int? CategoryId { get; set; }

    public int? EngagementId { get; set; }

    public int? LocationId { get; set; }

    public string SortBy { get; set; }

    public bool IsAscending { get; set; }

    public bool SpecifiedSalary { get; set; }

    public bool Intership { get; set; }
}
