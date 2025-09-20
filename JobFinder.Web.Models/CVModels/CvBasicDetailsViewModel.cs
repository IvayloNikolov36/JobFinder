using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.Web.Models.CvModels;

public class CvBasicDetailsViewModel : IMapFrom<CvBasicDetailsDTO>
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string PictureUrl { get; set; }
}
