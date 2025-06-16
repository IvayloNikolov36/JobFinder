using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;
using System.Collections.Generic;

namespace JobFinder.Web.Models.JobAds;

public class JobAdCriteriasViewModel : IMapFrom<JobAdCriteriasDTO>, IMapTo<JobAdCriteriasDTO>
{
    public string Position { get; set; }

    public int CityId { get; set; }

    public int JobCategoryId { get; set; }

    public int JobEngagementId { get; set; }

    public int WorkplaceTypeId { get; set; }

    public bool Intership { get; set; }

    public IEnumerable<int> SoftSkills { get; set; }

    public IEnumerable<int> TechStacks { get; set; }

    public IEnumerable<int> ITAreas { get; set; }
}
