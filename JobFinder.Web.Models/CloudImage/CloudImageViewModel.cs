using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;

namespace JobFinder.Web.Models.CloudImage;

public class CloudImageViewModel : IMapFrom<CloudImageDTO>
{
    public int Id { get; set; }

    public string Url { get; set; }

    public string ThumbnailUrl { get; set; }
}
