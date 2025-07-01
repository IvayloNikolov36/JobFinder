namespace JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;

public class CvPreviewRequestListingDTO
{
    public int Id { get; set; }

    // TODO: use dto
    public int JobAdId { get; set; }
    public string Position { get; set; }

    // TODO: use dto
    public string CompanyName { get; set; }
    public string CompanyLogoUrl { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
