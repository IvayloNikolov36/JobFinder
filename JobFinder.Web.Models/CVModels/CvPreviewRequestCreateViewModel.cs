using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using System;

namespace JobFinder.Web.Models.CvModels;

public class CvPreviewRequestCreateViewModel : IMapTo<CvPreviewRequestDTO>
{
    public Guid AnonymousProfileId { get; set; }

    public int JobAdId { get; set; }
}
