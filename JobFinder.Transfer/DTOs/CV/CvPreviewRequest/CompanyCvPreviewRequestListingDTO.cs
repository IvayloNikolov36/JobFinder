namespace JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;

public class CompanyCvPreviewRequestListingDTO
{
    public int Id { get; set; }

    public string AnonymousProfileId { get; set; }

    public string CvId { get; set; }

    // TODO: use DTO
    public int JobAdId { get; set; }
    public string Position { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
