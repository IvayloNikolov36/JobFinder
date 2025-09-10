using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.Web.Models.JobAds;

public class JobAdBasicDetailsViewModel : IMapFrom<JobAdBasicDetailsDTO>
{
    public int Id { get; set; }

    public string Position { get; set; }
}
