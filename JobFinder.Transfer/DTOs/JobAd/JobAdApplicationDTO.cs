namespace JobFinder.Transfer.DTOs.JobAd;

public class JobAdApplicationDTO
{
    public int Id { get; set; }

    public int JobAdId { get; set; }

    public string CvId { get; set; }

    public string CvName { get; set; }

    public string JobTitle { get; set; }

    public string CompanyName { get; set; }

    public int? CompanyLogoId { get; set; }

    public DateTime AppliedOn { get; set; }

    public DateTime? PreviewDate { get; set; }
}
