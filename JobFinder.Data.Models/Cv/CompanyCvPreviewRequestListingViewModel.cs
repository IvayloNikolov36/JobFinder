using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;
using System;

namespace JobFinder.Data.Models.Cv;

public class CompanyCvPreviewRequestListingViewModel : IMapFrom<CompanyCvPreviewRequestListingDTO>
{
    public string AnonymousProfileId { get; set; }

    public string CvId { get; set; }

    public int JobAdId { get; set; }

    public string Position { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
