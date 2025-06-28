using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using System;

namespace JobFinder.Web.Models.CvModels;

public class CvPreviewRequestListingViewModel : IMapFrom<CvPreviewRequestListingDTO>
{
    public string AnonymousProfileId { get; set; }

    public int JobAdId { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
