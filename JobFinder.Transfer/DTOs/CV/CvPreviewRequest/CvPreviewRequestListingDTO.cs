using JobFinder.Transfer.DTOs.Company;
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;

public class CvPreviewRequestListingDTO
{
    public int Id { get; set; }

    public JobAdBasicDetailsDTO JobAd { get; set; }

    public CompanyBasicDetailsDTO Company { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
