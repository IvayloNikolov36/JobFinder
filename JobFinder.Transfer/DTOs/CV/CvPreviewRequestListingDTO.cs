namespace JobFinder.Transfer.DTOs.Cv;

public class CvPreviewRequestListingDTO
{
    public int Id { get; set; }

    public int JobAdId { get; set; }

    public string Position { get; set; }

    public string CompanyName { get; set; }

    public string CompanyLogoUrl { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
