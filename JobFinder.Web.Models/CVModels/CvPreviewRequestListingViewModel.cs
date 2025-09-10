using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;
using JobFinder.Web.Models.Company;
using JobFinder.Web.Models.JobAds;
using System;

namespace JobFinder.Web.Models.CvModels;

public class CvPreviewRequestListingViewModel : IMapFrom<CvPreviewRequestListingDTO>
{
    public int Id { get; set; }

    public JobAdBasicDetailsViewModel JobAd { get; set; }

    public CompanyBasicDetailsViewModel Company { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
