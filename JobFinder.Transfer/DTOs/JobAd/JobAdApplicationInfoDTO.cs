namespace JobFinder.Transfer.DTOs.JobAd;

public class JobAdApplicationInfoDTO
{
    public int Id { get; set; }

    public int JobAdId { get; set; }

    public string Applicant { get; set; }

    // TODO: use a DTO
    public string CvId { get; set; }

    public string CvName { get; set; }

    public string CvPictureUrl { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public DateTime AppliedOn { get; set; }

    public bool IsPreviewed { get; set; }

    public DateTime? PreviewDate { get; set; }
}
