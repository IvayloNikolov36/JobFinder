using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;
using JobFinder.Web.Models.JobAds;
using System;

namespace JobFinder.Web.Models.CvModels;

public class CompanyCvPreviewRequestListingViewModel : IMapFrom<CompanyCvPreviewRequestListingDTO>
{
    public int Id { get; set; }

    public string CvId { get; set; }

    public JobAdBasicDetailsViewModel JobAd { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
