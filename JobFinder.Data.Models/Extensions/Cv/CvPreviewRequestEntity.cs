using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.Data.Models.Cv;

public partial class CvPreviewRequestEntity : IMapFrom<CvPreviewRequestDTO>,
    IMapTo<CvPreviewRequestListingDTO>
{
}
