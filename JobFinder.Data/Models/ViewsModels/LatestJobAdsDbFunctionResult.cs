using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;

namespace JobFinder.Data.Models.ViewsModels;

public class LatestJobAdsDbFunctionResult : IMapTo<LatestJobAdsDTO>
{
    public int CompanyId { get; set; }

    public string JobAdsIds { get; set; }
}
