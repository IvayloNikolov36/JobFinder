namespace JobFinder.Transfer.DTOs.Cv;

public class CvPreviewRequestListingDTO
{
    public string AnonymousProfileId { get; set; }

    public int JobAdId { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
