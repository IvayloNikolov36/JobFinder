namespace JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;

public class CvPreviewRequestDTO
{
    public string RequesterId { get; set; }

    public Guid AnonymousProfileId { get; set; }

    public int JobAdId { get; set; }
}
