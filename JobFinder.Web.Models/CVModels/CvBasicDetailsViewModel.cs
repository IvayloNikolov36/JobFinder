using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;

namespace JobFinder.Web.Models.CvModels;

public class CvBasicDetailsViewModel : BasicViewModel, IMapFrom<CvBasicDetailsDTO>
{
    public string PictureUrl { get; set; }
}
