using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;

public class CompanyCvPreviewRequestListingDTO
{
    public int Id { get; set; }

    public string CvId { get; set; }

    public JobAdBasicDetailsDTO JobAd { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
